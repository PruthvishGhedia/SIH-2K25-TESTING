import requests

BASE_URL = "http://localhost:5000"
TIMEOUT = 30
HEADERS = {"Content-Type": "application/json"}

def test_fee_payment_processing_receipt_generation_and_reporting():
    fee_id = None
    student_id = None
    try:
        # Create a student to associate fee with
        student_payload = {"first_name": "FeeTest", "last_name": "User", "email": "fee.user@example.com"}
        student_resp = requests.post(
            f"{BASE_URL}/api/Student",
            json=student_payload,
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert student_resp.status_code == 201, f"Create student failed: {student_resp.text}"
        student_id = student_resp.json().get("student_id")
        assert student_id, "student_id not returned"

        # Create a fee record using FeesController model
        fee_payload = {
            "student_id": int(student_id),
            "fee_type": "Tuition",
            "amount": 1500.0
        }
        fee_resp = requests.post(
            f"{BASE_URL}/api/Fees",
            json=fee_payload,
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert fee_resp.status_code == 201, f"Fee creation failed: {fee_resp.text}"
        fee_data = fee_resp.json()
        fee_id = fee_data.get("fee_id")
        assert fee_id, "fee_id not returned"

        # Retrieve the fee
        get_resp = requests.get(
            f"{BASE_URL}/api/Fees/{fee_id}",
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert get_resp.status_code == 200, f"Get fee failed: {get_resp.text}"
        got = get_resp.json()
        assert int(got.get("student_id")) == int(student_id)
        assert got.get("fee_type") == fee_payload["fee_type"]
        assert float(got.get("amount")) == fee_payload["amount"]

    finally:
        if fee_id:
            try:
                requests.delete(
                    f"{BASE_URL}/api/Fees/{fee_id}",
                    headers=HEADERS,
                    timeout=TIMEOUT
                )
            except Exception:
                pass
        if student_id:
            try:
                requests.delete(
                    f"{BASE_URL}/api/Student/{student_id}",
                    headers=HEADERS,
                    timeout=TIMEOUT
                )
            except Exception:
                pass

test_fee_payment_processing_receipt_generation_and_reporting()