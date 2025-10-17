import requests
from datetime import datetime, timedelta

BASE_URL = "http://localhost:5000"
TIMEOUT = 30
HEADERS = {"Content-Type": "application/json"}

def test_exam_scheduling_result_entry_and_publication():
    exam_id = None
    try:
        # Create an exam using ExamController model
        exam_payload = {
            "dept_id": 1,
            "subject_code": 101,
            "exam_date": (datetime.utcnow() + timedelta(days=7)).isoformat() + "Z",
            "assessment_type": "Midterm"
        }
        create_resp = requests.post(
            f"{BASE_URL}/api/Exam",
            json=exam_payload,
            headers=HEADERS,
            timeout=TIMEOUT,
        )
        assert create_resp.status_code == 201, f"Create exam failed: {create_resp.text}"
        created = create_resp.json()
        exam_id = created.get("exam_id")
        assert exam_id is not None, "exam_id not returned"

        # Retrieve the exam
        get_resp = requests.get(
            f"{BASE_URL}/api/Exam/{exam_id}",
            headers=HEADERS,
            timeout=TIMEOUT,
        )
        assert get_resp.status_code == 200, f"Get exam failed: {get_resp.text}"
        exam = get_resp.json()
        assert exam.get("dept_id") == exam_payload["dept_id"]
        assert exam.get("subject_code") == exam_payload["subject_code"]
        assert exam.get("assessment_type") == exam_payload["assessment_type"]

        # Update the exam
        update_payload = dict(exam_payload)
        update_payload["assessment_type"] = "Midterm-Updated"
        upd_resp = requests.put(
            f"{BASE_URL}/api/Exam/{exam_id}",
            json=update_payload,
            headers=HEADERS,
            timeout=TIMEOUT,
        )
        assert upd_resp.status_code == 200, f"Update exam failed: {upd_resp.text}"
        upd = upd_resp.json()
        assert upd.get("assessment_type") == "Midterm-Updated"

    finally:
        if exam_id:
            requests.delete(
                f"{BASE_URL}/api/Exam/{exam_id}",
                headers=HEADERS,
                timeout=TIMEOUT,
            )

test_exam_scheduling_result_entry_and_publication()
