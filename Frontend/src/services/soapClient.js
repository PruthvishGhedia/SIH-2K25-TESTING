/* Central SOAP client for all entities. Uses fetch + XML envelopes. Exposes standardized methods: list(entity, {limit,offset,filters}), get(entity,id), create(entity,payload), update(entity,id,payload), remove(entity,id) */
const BACKEND_BASE = process.env.REACT_APP_BACKEND_BASE || process.env.VITE_BACKEND_BASE || 'http://localhost:5000';

function buildEnvelope(actionBody) {
  return `<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>${actionBody}</soap:Body>
</soap:Envelope>`;
}

async function soapFetch(path, soapAction, bodyXml) {
  const url = `${BACKEND_BASE}${path}`;
  const envelope = buildEnvelope(bodyXml);
  const res = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'text/xml; charset=utf-8',
      'SOAPAction': soapAction
    },
    body: envelope,
    credentials: 'include'
  });
  const text = await res.text();
  if (!res.ok) {
    throw new Error(`SOAP call failed: ${res.status} - ${text}`);
  }
  return text;
}

function parseSoapBody(xmlText) {
  // Minimal helper: parse XML -> DOM -> extract first child of Body
  const parser = new DOMParser();
  const doc = parser.parseFromString(xmlText, 'application/xml');
  const body = doc.getElementsByTagNameNS('http://schemas.xmlsoap.org/soap/envelope/','Body')[0] || doc.getElementsByTagName('soap:Body')[0];
  if (!body) return null;
  // return inner XML of body
  return body.firstElementChild ? body.firstElementChild.outerHTML : body.textContent;
}

// Example helper to map XML to JSON for simple responses. For complex nested types, adjust mapping or use an XML->JSON library.
function xmlToJson(xmlNode) {
  // very small xml->json converter for simple nodes
  const obj = {};
  if (!xmlNode) return obj;
  Array.from(xmlNode.childNodes).forEach(node => {
    if (node.nodeType === Node.TEXT_NODE) {
      const val = node.nodeValue.trim(); if (val) obj['value'] = val;
    } else if (node.nodeType === Node.ELEMENT_NODE) {
      const key = node.localName || node.nodeName;
      const child = xmlToJson(node);
      if (obj[key]) {
        if (!Array.isArray(obj[key])) obj[key] = [obj[key]];
        obj[key].push(child);
      } else obj[key] = child;
    }
  });
  return obj;
}

function entityPath(entity){ return `/soap/${entity}`; }
function idField(entity){ return getEntityIdField(entity); }

export default {
  async list(entity, { limit = 50, offset = 0 } = {}) {
    const path = entityPath(entity);
    const soapAction = `http://tempuri.org/I${capitalize(entity)}Service/ListAsync`;
    const bodyXml = `<ListAsync xmlns="http://tempuri.org/"><limit>${limit}</limit><offset>${offset}</offset></ListAsync>`;
    const raw = await soapFetch(path, soapAction, bodyXml);
    const body = parseSoapBody(raw);
    const parser = new DOMParser();
    const doc = parser.parseFromString(body, 'application/xml');
    const items = [];
    const nodes = doc.children;
    for (let i=0;i<nodes.length;i++) {
      items.push(xmlToJson(nodes[i]));
    }
    return items;
  },
  async get(entity, id) {
    const path = entityPath(entity);
    const soapAction = `http://tempuri.org/I${capitalize(entity)}Service/GetAsync`;
    const bodyXml = `<GetAsync xmlns="http://tempuri.org/"><${getEntityIdField(entity)}>${id}</${getEntityIdField(entity)}></GetAsync>`;
    const raw = await soapFetch(path, soapAction, bodyXml);
    const body = parseSoapBody(raw);
    const parser = new DOMParser();
    const doc = parser.parseFromString(body, 'application/xml');
    return xmlToJson(doc.documentElement);
  },
  async create(entity, payload) {
    const path = entityPath(entity);
    const soapAction = `http://tempuri.org/I${capitalize(entity)}Service/CreateAsync`;
    const payloadXml = objectToXml(payload, entity);
    const bodyXml = `<CreateAsync xmlns="http://tempuri.org/"><item>${payloadXml}</item></CreateAsync>`;
    const raw = await soapFetch(path, soapAction, bodyXml);
    const body = parseSoapBody(raw);
    const parser = new DOMParser();
    const doc = parser.parseFromString(body, 'application/xml');
    return xmlToJson(doc.documentElement);
  },
  async update(entity, id, payload) {
    const path = entityPath(entity);
    const soapAction = `http://tempuri.org/I${capitalize(entity)}Service/UpdateAsync`;
    const payloadXml = objectToXml(payload, entity);
    const bodyXml = `<UpdateAsync xmlns="http://tempuri.org/"><${getEntityIdField(entity)}>${id}</${getEntityIdField(entity)}><item>${payloadXml}</item></UpdateAsync>`;
    const raw = await soapFetch(path, soapAction, bodyXml);
    const body = parseSoapBody(raw);
    const parser = new DOMParser();
    const doc = parser.parseFromString(body, 'application/xml');
    return xmlToJson(doc.documentElement);
  },
  async remove(entity, id) {
    const path = `/soap/${entity}`;
    const soapAction = `http://tempuri.org/I${capitalize(entity)}Service/RemoveAsync`;
    const bodyXml = `<RemoveAsync xmlns="http://tempuri.org/"><${getEntityIdField(entity)}>${id}</${getEntityIdField(entity)}></RemoveAsync>`;
    const raw = await soapFetch(path, soapAction, bodyXml);
    const body = parseSoapBody(raw);
    const parser = new DOMParser();
    const doc = parser.parseFromString(body, 'application/xml');
    // Expecting boolean or status
    return doc.documentElement ? doc.documentElement.textContent : null;
  }
};

function capitalize(s) { return s.charAt(0).toUpperCase() + s.slice(1); }

function getEntityIdField(entity) {
  const idFields = {
    'student': 'student_id',
    'course': 'course_id',
    'department': 'dept_id',
    'user': 'user_id',
    'fees': 'fee_id',
    'exam': 'exam_id',
    'guardian': 'guardian_id',
    'admission': 'admission_id',
    'hostel': 'hostel_id',
    'room': 'room_id',
    'hostelallocation': 'allocation_id',
    'library': 'book_id',
    'bookissue': 'issue_id',
    'result': 'result_id',
    'userrole': 'userrole_id',
    'contactdetails': 'contact_id'
  };
  return idFields[entity.toLowerCase()] || `${entity}_id`;
}

function objectToXml(obj, rootName) {
  // basic serializer: for each key -> <key>value</key>
  if (!obj) return '';
  let xml = '';
  Object.entries(obj).forEach(([k,v]) => {
    if (v === null || v === undefined) return;
    if (Array.isArray(v)) {
      v.forEach(item => { xml += `<${k}>${escapeXml(item)}</${k}>`; });
    } else if (typeof v === 'object') {
      xml += `<${k}>${objectToXml(v)}</${k}>`;
    } else {
      xml += `<${k}>${escapeXml(v)}</${k}>`;
    }
  });
  return xml;
}

function escapeXml(unsafe) { 
  if(unsafe === null || unsafe === undefined) return ''; 
  return String(unsafe).replace(/[<>&'"]/g, function (c) { 
    return { '<': '&lt;', '>': '&gt;', '&': '&amp;', "'": '&apos;', '"': '&quot;' }[c]; 
  }); 
}