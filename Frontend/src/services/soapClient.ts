import { 
  Student, Course, Department, User, Fees, Exam,
  CreateStudentRequest, CreateCourseRequest, CreateDepartmentRequest,
  CreateUserRequest, CreateFeesRequest, CreateExamRequest,
  ApiResponse, PaginatedResponse
} from '../types';

const BASE_URL = 'http://localhost:5000'; // Adjust based on your backend URL

export async function soapRequest(endpoint: string, action: string, xml: string): Promise<string> {
  const res = await fetch(`${BASE_URL}${endpoint}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'text/xml; charset=utf-8',
      'SOAPAction': action
    },
    body: xml
  })
  
  if (!res.ok) {
    throw new Error(`SOAP request failed: ${res.status} ${res.statusText}`);
  }
  
  const text = await res.text()
  return text
}

// Helper function to create SOAP XML for different operations
function createSoapXml(operation: string, params: Record<string, any>): string {
  const paramsXml = Object.entries(params)
    .map(([key, value]) => `<${key}>${value}</${key}>`)
    .join('');
    
  return `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <${operation} xmlns="http://tempuri.org/">
          ${paramsXml}
        </${operation}>
      </soap:Body>
    </soap:Envelope>`;
}

// Student Service
export const studentService = {
  async list(limit = 100, offset = 0): Promise<Student[]> {
    const xml = createSoapXml('ListAsync', { limit, offset });
    const response = await soapRequest('/soap/StudentService.svc', 'http://tempuri.org/IStudentService/ListAsync', xml);
    // Parse SOAP response and return Student[]
    return parseSoapResponse(response) as Student[];
  },

  async get(id: number): Promise<Student | null> {
    const xml = createSoapXml('GetAsync', { student_id: id });
    const response = await soapRequest('/soap/StudentService.svc', 'http://tempuri.org/IStudentService/GetAsync', xml);
    return parseSoapResponse(response) as Student | null;
  },

  async create(data: CreateStudentRequest): Promise<Student> {
    const xml = createSoapXml('CreateAsync', { item: JSON.stringify(data) });
    const response = await soapRequest('/soap/StudentService.svc', 'http://tempuri.org/IStudentService/CreateAsync', xml);
    return parseSoapResponse(response) as Student;
  },

  async update(id: number, data: CreateStudentRequest): Promise<Student | null> {
    const xml = createSoapXml('UpdateAsync', { student_id: id, item: JSON.stringify(data) });
    const response = await soapRequest('/soap/StudentService.svc', 'http://tempuri.org/IStudentService/UpdateAsync', xml);
    return parseSoapResponse(response) as Student | null;
  },

  async remove(id: number): Promise<Student | null> {
    const xml = createSoapXml('RemoveAsync', { student_id: id });
    const response = await soapRequest('/soap/StudentService.svc', 'http://tempuri.org/IStudentService/RemoveAsync', xml);
    return parseSoapResponse(response) as Student | null;
  }
};

// Course Service
export const courseService = {
  async list(limit = 100, offset = 0): Promise<Course[]> {
    const xml = createSoapXml('ListAsync', { limit, offset });
    const response = await soapRequest('/soap/CourseService.svc', 'http://tempuri.org/ICourseService/ListAsync', xml);
    return parseSoapResponse(response) as Course[];
  },

  async get(id: number): Promise<Course | null> {
    const xml = createSoapXml('GetAsync', { course_id: id });
    const response = await soapRequest('/soap/CourseService.svc', 'http://tempuri.org/ICourseService/GetAsync', xml);
    return parseSoapResponse(response) as Course | null;
  },

  async create(data: CreateCourseRequest): Promise<Course> {
    const xml = createSoapXml('CreateAsync', { item: JSON.stringify(data) });
    const response = await soapRequest('/soap/CourseService.svc', 'http://tempuri.org/ICourseService/CreateAsync', xml);
    return parseSoapResponse(response) as Course;
  },

  async update(id: number, data: CreateCourseRequest): Promise<Course | null> {
    const xml = createSoapXml('UpdateAsync', { course_id: id, item: JSON.stringify(data) });
    const response = await soapRequest('/soap/CourseService.svc', 'http://tempuri.org/ICourseService/UpdateAsync', xml);
    return parseSoapResponse(response) as Course | null;
  },

  async remove(id: number): Promise<Course | null> {
    const xml = createSoapXml('RemoveAsync', { course_id: id });
    const response = await soapRequest('/soap/CourseService.svc', 'http://tempuri.org/ICourseService/RemoveAsync', xml);
    return parseSoapResponse(response) as Course | null;
  }
};

// Department Service
export const departmentService = {
  async list(limit = 100, offset = 0): Promise<Department[]> {
    const xml = createSoapXml('ListAsync', { limit, offset });
    const response = await soapRequest('/soap/DepartmentService.svc', 'http://tempuri.org/IDepartmentService/ListAsync', xml);
    return parseSoapResponse(response) as Department[];
  },

  async get(id: number): Promise<Department | null> {
    const xml = createSoapXml('GetAsync', { dept_id: id });
    const response = await soapRequest('/soap/DepartmentService.svc', 'http://tempuri.org/IDepartmentService/GetAsync', xml);
    return parseSoapResponse(response) as Department | null;
  },

  async create(data: CreateDepartmentRequest): Promise<Department> {
    const xml = createSoapXml('CreateAsync', { item: JSON.stringify(data) });
    const response = await soapRequest('/soap/DepartmentService.svc', 'http://tempuri.org/IDepartmentService/CreateAsync', xml);
    return parseSoapResponse(response) as Department;
  },

  async update(id: number, data: CreateDepartmentRequest): Promise<Department | null> {
    const xml = createSoapXml('UpdateAsync', { dept_id: id, item: JSON.stringify(data) });
    const response = await soapRequest('/soap/DepartmentService.svc', 'http://tempuri.org/IDepartmentService/UpdateAsync', xml);
    return parseSoapResponse(response) as Department | null;
  },

  async remove(id: number): Promise<Department | null> {
    const xml = createSoapXml('RemoveAsync', { dept_id: id });
    const response = await soapRequest('/soap/DepartmentService.svc', 'http://tempuri.org/IDepartmentService/RemoveAsync', xml);
    return parseSoapResponse(response) as Department | null;
  }
};

// User Service
export const userService = {
  async list(limit = 100, offset = 0): Promise<User[]> {
    const xml = createSoapXml('ListAsync', { limit, offset });
    const response = await soapRequest('/soap/UserService.svc', 'http://tempuri.org/IUserService/ListAsync', xml);
    return parseSoapResponse(response) as User[];
  },

  async get(id: number): Promise<User | null> {
    const xml = createSoapXml('GetAsync', { user_id: id });
    const response = await soapRequest('/soap/UserService.svc', 'http://tempuri.org/IUserService/GetAsync', xml);
    return parseSoapResponse(response) as User | null;
  },

  async create(data: CreateUserRequest): Promise<User> {
    const xml = createSoapXml('CreateAsync', { item: JSON.stringify(data) });
    const response = await soapRequest('/soap/UserService.svc', 'http://tempuri.org/IUserService/CreateAsync', xml);
    return parseSoapResponse(response) as User;
  },

  async update(id: number, data: CreateUserRequest): Promise<User | null> {
    const xml = createSoapXml('UpdateAsync', { user_id: id, item: JSON.stringify(data) });
    const response = await soapRequest('/soap/UserService.svc', 'http://tempuri.org/IUserService/UpdateAsync', xml);
    return parseSoapResponse(response) as User | null;
  },

  async remove(id: number): Promise<User | null> {
    const xml = createSoapXml('RemoveAsync', { user_id: id });
    const response = await soapRequest('/soap/UserService.svc', 'http://tempuri.org/IUserService/RemoveAsync', xml);
    return parseSoapResponse(response) as User | null;
  }
};

// Fees Service
export const feesService = {
  async list(limit = 100, offset = 0): Promise<Fees[]> {
    const xml = createSoapXml('ListAsync', { limit, offset });
    const response = await soapRequest('/soap/FeesService.svc', 'http://tempuri.org/IFeesService/ListAsync', xml);
    return parseSoapResponse(response) as Fees[];
  },

  async get(id: number): Promise<Fees | null> {
    const xml = createSoapXml('GetAsync', { fees_id: id });
    const response = await soapRequest('/soap/FeesService.svc', 'http://tempuri.org/IFeesService/GetAsync', xml);
    return parseSoapResponse(response) as Fees | null;
  },

  async create(data: CreateFeesRequest): Promise<Fees> {
    const xml = createSoapXml('CreateAsync', { item: JSON.stringify(data) });
    const response = await soapRequest('/soap/FeesService.svc', 'http://tempuri.org/IFeesService/CreateAsync', xml);
    return parseSoapResponse(response) as Fees;
  },

  async update(id: number, data: CreateFeesRequest): Promise<Fees | null> {
    const xml = createSoapXml('UpdateAsync', { fees_id: id, item: JSON.stringify(data) });
    const response = await soapRequest('/soap/FeesService.svc', 'http://tempuri.org/IFeesService/UpdateAsync', xml);
    return parseSoapResponse(response) as Fees | null;
  },

  async remove(id: number): Promise<Fees | null> {
    const xml = createSoapXml('RemoveAsync', { fees_id: id });
    const response = await soapRequest('/soap/FeesService.svc', 'http://tempuri.org/IFeesService/RemoveAsync', xml);
    return parseSoapResponse(response) as Fees | null;
  }
};

// Exam Service
export const examService = {
  async list(limit = 100, offset = 0): Promise<Exam[]> {
    const xml = createSoapXml('ListAsync', { limit, offset });
    const response = await soapRequest('/soap/ExamService.svc', 'http://tempuri.org/IExamService/ListAsync', xml);
    return parseSoapResponse(response) as Exam[];
  },

  async get(id: number): Promise<Exam | null> {
    const xml = createSoapXml('GetAsync', { exam_id: id });
    const response = await soapRequest('/soap/ExamService.svc', 'http://tempuri.org/IExamService/GetAsync', xml);
    return parseSoapResponse(response) as Exam | null;
  },

  async create(data: CreateExamRequest): Promise<Exam> {
    const xml = createSoapXml('CreateAsync', { item: JSON.stringify(data) });
    const response = await soapRequest('/soap/ExamService.svc', 'http://tempuri.org/IExamService/CreateAsync', xml);
    return parseSoapResponse(response) as Exam;
  },

  async update(id: number, data: CreateExamRequest): Promise<Exam | null> {
    const xml = createSoapXml('UpdateAsync', { exam_id: id, item: JSON.stringify(data) });
    const response = await soapRequest('/soap/ExamService.svc', 'http://tempuri.org/IExamService/UpdateAsync', xml);
    return parseSoapResponse(response) as Exam | null;
  },

  async remove(id: number): Promise<Exam | null> {
    const xml = createSoapXml('RemoveAsync', { exam_id: id });
    const response = await soapRequest('/soap/ExamService.svc', 'http://tempuri.org/IExamService/RemoveAsync', xml);
    return parseSoapResponse(response) as Exam | null;
  }
};

// Helper function to parse SOAP response
function parseSoapResponse(response: string): any {
  try {
    // This is a simplified parser - in a real application, you'd want to use a proper XML parser
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(response, 'text/xml');
    
    // Extract the result from the SOAP response
    const resultElement = xmlDoc.querySelector('*[xmlns*="tempuri.org"]')?.firstElementChild;
    if (resultElement) {
      const textContent = resultElement.textContent;
      if (textContent) {
        try {
          return JSON.parse(textContent);
        } catch {
          return textContent;
        }
      }
    }
    
    return null;
  } catch (error) {
    console.error('Error parsing SOAP response:', error);
    return null;
  }
}

// Health check
export async function healthCheck(): Promise<boolean> {
  try {
    const response = await fetch(`${BASE_URL}/health`);
    return response.ok;
  } catch {
    return false;
  }
}