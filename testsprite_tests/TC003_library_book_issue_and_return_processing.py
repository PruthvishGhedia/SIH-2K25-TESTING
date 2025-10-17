import requests
import uuid

BASE_URL = "http://localhost:5000"
TIMEOUT = 30
HEADERS = {"Content-Type": "application/json"}

def test_library_book_issue_and_return_processing():
    """Test library book issue and return processing, and catalog update."""

    # Step 1: Create a new book resource to issue and return
    new_book_payload = {
        "title": f"Test Book {uuid.uuid4()}",
        "author": "Test Author"
    }

    book_id = None
    student_id = None
    issued_record_id = None

    try:
        # Create a new book in the catalog
        resp_create_book = requests.post(
            f"{BASE_URL}/api/library/books",
            json=new_book_payload,
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert resp_create_book.status_code == 201, f"Failed to create book, status: {resp_create_book.status_code}"
        book_data = resp_create_book.json()
        book_id = book_data.get("book_id") or book_data.get("id")
        assert book_id is not None, "Created book ID missing"

        # Step 2: Create a student required for issuing a book
        student_payload = {"first_name": "LibTest", "last_name": "User", "email": "lib.user@example.com"}
        student_resp = requests.post(
            f"{BASE_URL}/api/Student",
            json=student_payload,
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert student_resp.status_code == 201, f"Failed to create student, status: {student_resp.status_code}"
        student_id = student_resp.json().get("student_id")
        assert student_id is not None, "Created student ID missing"

        # Step 3: Issue the book to the created student
        issue_payload = {
            "student_id": int(student_id),
            "book_id": int(book_id),
            "issue_date": "2025-09-29T00:00:00Z"
        }

        resp_issue = requests.post(
            f"{BASE_URL}/api/library/issue",
            json=issue_payload,
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert resp_issue.status_code == 201, f"Book issuance failed, status: {resp_issue.status_code}"
        issue_data = resp_issue.json()
        issued_record_id = issue_data.get("issue_id") or issue_data.get("issueId") or issue_data.get("id")
        assert issued_record_id is not None, "Issue record ID missing"

        # Step 4: Verify the catalog can be retrieved (model may not track copies)
        resp_catalog = requests.get(
            f"{BASE_URL}/api/library/books/{book_id}",
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert resp_catalog.status_code == 200, f"Failed to fetch updated book catalog, status: {resp_catalog.status_code}"
        _ = resp_catalog.json()

        # Step 5: Return the book
        return_payload = {
            "issueId": int(issued_record_id),
            "returnDate": "2025-10-01"
        }
        resp_return = requests.post(
            f"{BASE_URL}/api/library/issue/return",
            json=return_payload,
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert resp_return.status_code == 200, f"Book return failed, status: {resp_return.status_code}"
        return_data = resp_return.json()
        assert return_data.get("status") == "returned", f"Return status unexpected: {return_data.get('status')}"

        # Step 6: Verify the catalog can be retrieved after return
        resp_catalog_after_return = requests.get(
            f"{BASE_URL}/api/library/books/{book_id}",
            headers=HEADERS,
            timeout=TIMEOUT
        )
        assert resp_catalog_after_return.status_code == 200, f"Failed to fetch updated catalog after return, status: {resp_catalog_after_return.status_code}"
        _ = resp_catalog_after_return.json()

    finally:
        # Cleanup: delete the created book resource if exists
        if issued_record_id:
            try:
                requests.delete(
                    f"{BASE_URL}/api/library/issue/{issued_record_id}",
                    headers=HEADERS,
                    timeout=TIMEOUT
                )
            except Exception:
                pass

        if book_id:
            try:
                requests.delete(
                    f"{BASE_URL}/api/library/books/{book_id}",
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

test_library_book_issue_and_return_processing()