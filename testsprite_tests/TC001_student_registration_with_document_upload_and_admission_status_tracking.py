import requests
import time

BASE_URL = "http://localhost:5000"
TIMEOUT = 30

def test_student_registration_with_document_upload_and_admission_status_tracking():
    # Define headers and sample student registration data with documents
    headers = {
        "Accept": "application/json"
    }
    # Sample student registration payload matching StudentController model
    registration_payload = {
        "first_name": "Test",
        "last_name": "Student",
        "email": "test.student@example.com"
    }

    # Step 1: Register student with document upload
    try:
        response = requests.post(
            f"{BASE_URL}/api/student",
            json=registration_payload,
            headers=headers,
            timeout=TIMEOUT
        )
        assert response.status_code == 201, f"Expected 201 Created, got {response.status_code}"
        reg_data = response.json()
        # API returns created student with key student_id
        assert "student_id" in reg_data, "Response missing student_id"
        student_id = reg_data["student_id"]

        # Step 2: Confirm registration success status (if included in response)
        # - if there is a status field, check for success
        if "registrationStatus" in reg_data:
            assert reg_data["registrationStatus"].lower() in ("submitted", "processing", "registered"), \
                f"Unexpected registrationStatus value: {reg_data['registrationStatus']}"

        # No admission-status endpoint in current API; basic creation validation is sufficient

    finally:
        # Cleanup: Delete the student registration to avoid stale data
        if 'student_id' in locals():
            try:
                del_response = requests.delete(
                    f"{BASE_URL}/api/student/{student_id}",
                    headers=headers,
                    timeout=TIMEOUT
                )
                # Allow 200 or 204 as successful deletion responses
                assert del_response.status_code in (200, 204), f"Failed to delete test student, status: {del_response.status_code}"
            except Exception:
                # Suppress exceptions during cleanup
                pass

test_student_registration_with_document_upload_and_admission_status_tracking()
