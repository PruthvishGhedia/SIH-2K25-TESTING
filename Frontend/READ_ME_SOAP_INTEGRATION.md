# SOAP Integration Notes

Use src/services/soapClient.js to call the backend. Example usage:

```js
import soapClient from './services/soapClient';
const students = await soapClient.list('student', {limit: 20, offset: 0});
const student = await soapClient.get('student', 1);
await soapClient.create('student', { first_name: 'A', last_name: 'B', dob: '2000-01-01' });
```

Important: backend must accept POST requests at /soap/{entity} with text/xml body and SOAPAction header.

## Available Entities

The SOAP client supports the following entities:

- `student` - Student records
- `course` - Course information  
- `department` - Department data
- `user` - User accounts
- `fees` - Fee records
- `exam` - Exam schedules
- `guardian` - Guardian information
- `admission` - Admission records
- `hostel` - Hostel management
- `room` - Room management
- `hostelallocation` - Hostel allocations
- `library` - Library books
- `bookissue` - Book issues
- `result` - Exam results
- `userrole` - User roles
- `contactdetails` - Contact information

## API Methods

### list(entity, options)
List all records for an entity with pagination.

```js
const students = await soapClient.list('student', { limit: 10, offset: 0 });
```

### get(entity, id)
Get a single record by ID.

```js
const student = await soapClient.get('student', 123);
```

### create(entity, payload)
Create a new record.

```js
const newStudent = await soapClient.create('student', {
  first_name: 'John',
  last_name: 'Doe',
  email: 'john@example.com',
  dob: '2000-01-01'
});
```

### update(entity, id, payload)
Update an existing record.

```js
const updatedStudent = await soapClient.update('student', 123, {
  first_name: 'Jane',
  last_name: 'Smith'
});
```

### remove(entity, id)
Delete a record.

```js
await soapClient.remove('student', 123);
```

## Error Handling

The SOAP client will throw errors for:
- Network failures
- SOAP faults from the server
- Invalid responses

```js
try {
  const students = await soapClient.list('student');
} catch (error) {
  console.error('SOAP call failed:', error.message);
}
```

## Configuration

Set the backend URL using environment variables:

- `VITE_BACKEND_BASE` - Backend URL (default: http://localhost:5000)
- `REACT_APP_BACKEND_BASE` - Alternative for Create React App

## SOAP Envelope Format

The client automatically generates SOAP envelopes in this format:

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <OperationName xmlns="http://tempuri.org/">
      <!-- Parameters -->
    </OperationName>
  </soap:Body>
</soap:Envelope>
```

## Response Parsing

The client automatically parses SOAP responses and converts them to JavaScript objects. Complex nested objects are supported through the XML-to-JSON converter.