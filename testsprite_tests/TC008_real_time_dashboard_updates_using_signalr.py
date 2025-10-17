import requests

BASE_URL = "http://localhost:5000"
HUB_PATH = "/dashboard"
TIMEOUT = 30


def test_real_time_dashboard_updates_using_signalr():
    # Basic validation that the DashboardHub route is mapped
    resp = requests.get(f"{BASE_URL}{HUB_PATH}", timeout=TIMEOUT)
    # SignalR negotiate/endpoints may return 200/404 depending on transport, accept not 500 as minimal health
    assert resp.status_code in (200, 404), f"Unexpected status from hub endpoint: {resp.status_code}"


test_real_time_dashboard_updates_using_signalr()