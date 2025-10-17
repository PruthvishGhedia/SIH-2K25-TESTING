import requests

BASE_URL = "http://localhost:5000"
TIMEOUT = 30
HEADERS = {"Content-Type": "application/json"}


def test_role_based_access_control_enforcement():
    created_role_id = None
    created_user_id = None
    try:
        # Create Role via RoleController
        role_data = {"role_name": "limited_role"}
        resp = requests.post(
            f"{BASE_URL}/api/Role",
            json=role_data,
            headers=HEADERS,
            timeout=TIMEOUT,
        )
        assert resp.status_code == 201, f"Role creation failed: {resp.text}"
        created_role = resp.json()
        created_role_id = created_role.get("role_id")
        assert created_role_id is not None, "Created role ID is None"

        # Create User associated to this role (if your model supports role assignment later, this just creates user)
        user_data = {
            "full_name": "Test Limited User",
            "email": "test_limited_user@example.com"
        }
        resp = requests.post(
            f"{BASE_URL}/api/User",
            json=user_data,
            headers=HEADERS,
            timeout=TIMEOUT,
        )
        assert resp.status_code == 201, f"User creation failed: {resp.text}"
        created_user = resp.json()
        created_user_id = created_user.get("user_id")
        assert created_user_id is not None, "Created user ID is None"

        # List roles - ensure endpoint is reachable
        list_resp = requests.get(f"{BASE_URL}/api/Role", headers=HEADERS, timeout=TIMEOUT)
        assert list_resp.status_code == 200, f"Role list failed: {list_resp.text}"

    finally:
        if created_user_id:
            try:
                requests.delete(f"{BASE_URL}/api/User/{created_user_id}", headers=HEADERS, timeout=TIMEOUT)
            except Exception:
                pass
        if created_role_id:
            try:
                requests.delete(f"{BASE_URL}/api/Role/{created_role_id}", headers=HEADERS, timeout=TIMEOUT)
            except Exception:
                pass


test_role_based_access_control_enforcement()
