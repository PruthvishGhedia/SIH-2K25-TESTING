import axios from 'axios';

// Get base URL from environment variables
const BASE_URL = import.meta.env.VITE_BACKEND_URL || 'http://localhost:5000/soap';

// Helper function to parse XML response
const parseXmlResponse = (xmlString) => {
  try {
    // Simple XML parser - in a real application, you might want to use a more robust library
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xmlString, 'text/xml');
    return xmlDoc;
  } catch (error) {
    console.error('Error parsing XML:', error);
    throw new Error('Failed to parse XML response');
  }
};

// Helper function to build SOAP envelope
const buildSoapEnvelope = (action, body) => {
  return `<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    ${body}
  </soap:Body>
</soap:Envelope>`;
};

// Helper function to extract data from XML response
const extractDataFromXml = (xmlDoc, tagName) => {
  const elements = xmlDoc.getElementsByTagName(tagName);
  if (elements.length > 0) {
    return elements[0].textContent;
  }
  return null;
};

// Generic SOAP request function
const soapRequest = async (endpoint, action, body, soapAction) => {
  try {
    const soapEnvelope = buildSoapEnvelope(action, body);
    
    const response = await axios.post(`${BASE_URL}${endpoint}`, soapEnvelope, {
      headers: {
        'Content-Type': 'text/xml; charset=utf-8',
        'SOAPAction': `"${soapAction}"`
      }
    });

    const xmlDoc = parseXmlResponse(response.data);
    return xmlDoc;
  } catch (error) {
    console.error('SOAP request error:', error);
    if (error.response) {
      // Server responded with error status
      throw new Error(`Server error: ${error.response.status} - ${error.response.statusText}`);
    } else if (error.request) {
      // Request was made but no response received
      throw new Error('Network error: No response received from server');
    } else {
      // Something else happened
      throw new Error(`Request error: ${error.message}`);
    }
  }
};

// Generic CRUD operations for entities
export const soapClient = {
  // List entities with pagination
  list: async (entityEndpoint, limit = 10, offset = 0) => {
    try {
      const body = `
        <ListAsync xmlns="http://tempuri.org/">
          <limit>${limit}</limit>
          <offset>${offset}</offset>
        </ListAsync>
      `;
      
      const soapAction = `http://tempuri.org/I${entityEndpoint.replace('/', '').charAt(0).toUpperCase() + entityEndpoint.replace('/', '').slice(1)}Service/ListAsync`;
      const xmlDoc = await soapRequest(entityEndpoint, 'ListAsync', body, soapAction);
      
      // Extract data from XML (this would need to be adapted based on actual SOAP response structure)
      // For now, we'll return a mock structure
      return {
        data: [],
        total: 0
      };
    } catch (error) {
      throw new Error(`Failed to list ${entityEndpoint}: ${error.message}`);
    }
  },

  // Get entity by ID
  get: async (entityEndpoint, id) => {
    try {
      const body = `
        <GetByIdAsync xmlns="http://tempuri.org/">
          <id>${id}</id>
        </GetByIdAsync>
      `;
      
      const soapAction = `http://tempuri.org/I${entityEndpoint.replace('/', '').charAt(0).toUpperCase() + entityEndpoint.replace('/', '').slice(1)}Service/GetByIdAsync`;
      const xmlDoc = await soapRequest(entityEndpoint, 'GetByIdAsync', body, soapAction);
      
      // Extract data from XML (this would need to be adapted based on actual SOAP response structure)
      return {};
    } catch (error) {
      throw new Error(`Failed to get ${entityEndpoint} with id ${id}: ${error.message}`);
    }
  },

  // Create entity
  create: async (entityEndpoint, data) => {
    try {
      // Build XML body for create operation
      let dataFields = '';
      for (const [key, value] of Object.entries(data)) {
        dataFields += `<${key}>${value}</${key}>`;
      }
      
      const body = `
        <CreateAsync xmlns="http://tempuri.org/">
          ${dataFields}
        </CreateAsync>
      `;
      
      const soapAction = `http://tempuri.org/I${entityEndpoint.replace('/', '').charAt(0).toUpperCase() + entityEndpoint.replace('/', '').slice(1)}Service/CreateAsync`;
      const xmlDoc = await soapRequest(entityEndpoint, 'CreateAsync', body, soapAction);
      
      // Extract created entity data from XML
      return {};
    } catch (error) {
      throw new Error(`Failed to create ${entityEndpoint}: ${error.message}`);
    }
  },

  // Update entity
  update: async (entityEndpoint, id, data) => {
    try {
      // Build XML body for update operation
      let dataFields = `<id>${id}</id>`;
      for (const [key, value] of Object.entries(data)) {
        dataFields += `<${key}>${value}</${key}>`;
      }
      
      const body = `
        <UpdateAsync xmlns="http://tempuri.org/">
          ${dataFields}
        </UpdateAsync>
      `;
      
      const soapAction = `http://tempuri.org/I${entityEndpoint.replace('/', '').charAt(0).toUpperCase() + entityEndpoint.replace('/', '').slice(1)}Service/UpdateAsync`;
      const xmlDoc = await soapRequest(entityEndpoint, 'UpdateAsync', body, soapAction);
      
      // Extract updated entity data from XML
      return {};
    } catch (error) {
      throw new Error(`Failed to update ${entityEndpoint} with id ${id}: ${error.message}`);
    }
  },

  // Delete entity
  delete: async (entityEndpoint, id) => {
    try {
      const body = `
        <DeleteAsync xmlns="http://tempuri.org/">
          <id>${id}</id>
        </DeleteAsync>
      `;
      
      const soapAction = `http://tempuri.org/I${entityEndpoint.replace('/', '').charAt(0).toUpperCase() + entityEndpoint.replace('/', '').slice(1)}Service/DeleteAsync`;
      const xmlDoc = await soapRequest(entityEndpoint, 'DeleteAsync', body, soapAction);
      
      // Return success status
      return true;
    } catch (error) {
      throw new Error(`Failed to delete ${entityEndpoint} with id ${id}: ${error.message}`);
    }
  }
};

// Export individual services for backward compatibility
export const studentService = {
  list: (limit, offset) => soapClient.list('/student', limit, offset),
  get: (id) => soapClient.get('/student', id),
  create: (data) => soapClient.create('/student', data),
  update: (id, data) => soapClient.update('/student', id, data),
  remove: (id) => soapClient.delete('/student', id)
};

export const courseService = {
  list: (limit, offset) => soapClient.list('/course', limit, offset),
  get: (id) => soapClient.get('/course', id),
  create: (data) => soapClient.create('/course', data),
  update: (id, data) => soapClient.update('/course', id, data),
  remove: (id) => soapClient.delete('/course', id)
};

export const departmentService = {
  list: (limit, offset) => soapClient.list('/department', limit, offset),
  get: (id) => soapClient.get('/department', id),
  create: (data) => soapClient.create('/department', data),
  update: (id, data) => soapClient.update('/department', id, data),
  remove: (id) => soapClient.delete('/department', id)
};

export const userService = {
  list: (limit, offset) => soapClient.list('/user', limit, offset),
  get: (id) => soapClient.get('/user', id),
  create: (data) => soapClient.create('/user', data),
  update: (id, data) => soapClient.update('/user', id, data),
  remove: (id) => soapClient.delete('/user', id)
};

export const feesService = {
  list: (limit, offset) => soapClient.list('/fees', limit, offset),
  get: (id) => soapClient.get('/fees', id),
  create: (data) => soapClient.create('/fees', data),
  update: (id, data) => soapClient.update('/fees', id, data),
  remove: (id) => soapClient.delete('/fees', id)
};

export const examService = {
  list: (limit, offset) => soapClient.list('/exam', limit, offset),
  get: (id) => soapClient.get('/exam', id),
  create: (data) => soapClient.create('/exam', data),
  update: (id, data) => soapClient.update('/exam', id, data),
  remove: (id) => soapClient.delete('/exam', id)
};

export const guardianService = {
  list: (limit, offset) => soapClient.list('/guardian', limit, offset),
  get: (id) => soapClient.get('/guardian', id),
  create: (data) => soapClient.create('/guardian', data),
  update: (id, data) => soapClient.update('/guardian', id, data),
  remove: (id) => soapClient.delete('/guardian', id)
};

export const admissionService = {
  list: (limit, offset) => soapClient.list('/admission', limit, offset),
  get: (id) => soapClient.get('/admission', id),
  create: (data) => soapClient.create('/admission', data),
  update: (id, data) => soapClient.update('/admission', id, data),
  remove: (id) => soapClient.delete('/admission', id)
};

export const hostelService = {
  list: (limit, offset) => soapClient.list('/hostel', limit, offset),
  get: (id) => soapClient.get('/hostel', id),
  create: (data) => soapClient.create('/hostel', data),
  update: (id, data) => soapClient.update('/hostel', id, data),
  remove: (id) => soapClient.delete('/hostel', id)
};

export const roomService = {
  list: (limit, offset) => soapClient.list('/room', limit, offset),
  get: (id) => soapClient.get('/room', id),
  create: (data) => soapClient.create('/room', data),
  update: (id, data) => soapClient.update('/room', id, data),
  remove: (id) => soapClient.delete('/room', id)
};

export const hostelAllocationService = {
  list: (limit, offset) => soapClient.list('/hostelallocation', limit, offset),
  get: (id) => soapClient.get('/hostelallocation', id),
  create: (data) => soapClient.create('/hostelallocation', data),
  update: (id, data) => soapClient.update('/hostelallocation', id, data),
  remove: (id) => soapClient.delete('/hostelallocation', id)
};

export const libraryService = {
  list: (limit, offset) => soapClient.list('/library', limit, offset),
  get: (id) => soapClient.get('/library', id),
  create: (data) => soapClient.create('/library', data),
  update: (id, data) => soapClient.update('/library', id, data),
  remove: (id) => soapClient.delete('/library', id)
};

export const bookIssueService = {
  list: (limit, offset) => soapClient.list('/bookissue', limit, offset),
  get: (id) => soapClient.get('/bookissue', id),
  create: (data) => soapClient.create('/bookissue', data),
  update: (id, data) => soapClient.update('/bookissue', id, data),
  remove: (id) => soapClient.delete('/bookissue', id)
};

export const resultService = {
  list: (limit, offset) => soapClient.list('/result', limit, offset),
  get: (id) => soapClient.get('/result', id),
  create: (data) => soapClient.create('/result', data),
  update: (id, data) => soapClient.update('/result', id, data),
  remove: (id) => soapClient.delete('/result', id)
};

export const userRoleService = {
  list: (limit, offset) => soapClient.list('/userrole', limit, offset),
  get: (id) => soapClient.get('/userrole', id),
  create: (data) => soapClient.create('/userrole', data),
  update: (id, data) => soapClient.update('/userrole', id, data),
  remove: (id) => soapClient.delete('/userrole', id)
};

export const contactDetailsService = {
  list: (limit, offset) => soapClient.list('/contactdetails', limit, offset),
  get: (id) => soapClient.get('/contactdetails', id),
  create: (data) => soapClient.create('/contactdetails', data),
  update: (id, data) => soapClient.update('/contactdetails', id, data),
  remove: (id) => soapClient.delete('/contactdetails', id)
};

// Export faculty, enrollment, attendance, and payment services
export const facultyService = {
  list: (limit, offset) => soapClient.list('/faculty', limit, offset),
  get: (id) => soapClient.get('/faculty', id),
  create: (data) => soapClient.create('/faculty', data),
  update: (id, data) => soapClient.update('/faculty', id, data),
  remove: (id) => soapClient.delete('/faculty', id)
};

export const enrollmentService = {
  list: (limit, offset) => soapClient.list('/enrollment', limit, offset),
  get: (id) => soapClient.get('/enrollment', id),
  create: (data) => soapClient.create('/enrollment', data),
  update: (id, data) => soapClient.update('/enrollment', id, data),
  remove: (id) => soapClient.delete('/enrollment', id)
};

export const attendanceService = {
  list: (limit, offset) => soapClient.list('/attendance', limit, offset),
  get: (id) => soapClient.get('/attendance', id),
  create: (data) => soapClient.create('/attendance', data),
  update: (id, data) => soapClient.update('/attendance', id, data),
  remove: (id) => soapClient.delete('/attendance', id)
};

export const paymentService = {
  list: (limit, offset) => soapClient.list('/payment', limit, offset),
  get: (id) => soapClient.get('/payment', id),
  create: (data) => soapClient.create('/payment', data),
  update: (id, data) => soapClient.update('/payment', id, data),
  remove: (id) => soapClient.delete('/payment', id)
};

export default soapClient;