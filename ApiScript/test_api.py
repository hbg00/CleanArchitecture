import requests
import json

requests.packages.urllib3.disable_warnings()

BASE_URL = "https://localhost:7160/api"

# Function to test an API endpoint
def test_endpoint(method, endpoint, data=None, token=None):
    headers = {"Content-Type": "application/json"}
    if token:
        headers["Authorization"] = "Bearer " + token

    url = BASE_URL + endpoint
    try:
        if method == "GET":
            response = requests.get(url, headers=headers, verify=False)
        elif method == "POST":
            response = requests.post(url, headers=headers, data=json.dumps(data), verify=False)
        elif method == "PUT":
            response = requests.put(url, headers=headers, data=json.dumps(data), verify=False)
        elif method == "DELETE":
            response = requests.delete(url, headers=headers, verify=False)
        else:
            raise ValueError("Invalid method")

        # Check if the response status code is not in the 2xx range
        response.raise_for_status()

        # If the response content is empty, return None
        if not response.text:
            return None

        # Try to decode the response content as JSON
        return response.json()

    except requests.exceptions.RequestException as e:
        print(f"Error occurred: {e}")
        return None

login_data = {"Email": "admin@localhost.com", "Password": "Test1234$"}

# Testing Account Endpoints
print("Testing Account Endpoints")
login_response = test_endpoint("POST", "/Account/login", login_data)
print(login_response)

# Example login data
# Log in and get JWT token
login_response = test_endpoint("POST", "/Account/login", login_data)
token = login_response.get("token")

# Test endpoints
print("Testing Account Endpoints")
account_data = {
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "userName": "johndoe",
    "password": "Test1234$"
}
print(test_endpoint("POST", "/Account/register", account_data))

print("Testing Dish Endpoints")
print(test_endpoint("GET", "/1/Dish", token=token))

dish_data = {
    "name": "New Dish",
    "description": "Delicious",
    "price": 9.99,
    "restaurantId": 1
}

print(test_endpoint("POST", "/1/Dish", dish_data, token=token))

print(test_endpoint("GET", "/1/Dish/1", token=token))
updated_dish_data = {
    "id": 1,
    "name": "Updated Dish",
    "description": "Even more delicious",
    "price": 10.99,
    "restaurantId": 1
}
print(test_endpoint("PUT", "/1/Dish/1", updated_dish_data, token=token))
print(test_endpoint("DELETE", "/1/Dish/1", token=token))

print("Testing Restaurant Endpoints")
print(test_endpoint("GET", "/Restaurant", token=token))
restaurant_data = {
    "name": "New Restaurant",
    "description": "A great place to dine",
    "category": "Fine Dining",
    "hasDelivery": True,
    "contactEmail": "contact@newrestaurant.com",
    "contactName": "Jane Doe",
    "address": {
        "city": "Cityville",
        "street": "123 Main St",
        "postalCode": "12-345"
    }
}
print(test_endpoint("POST", "/Restaurant", restaurant_data, token=token))
print(test_endpoint("GET", "/Restaurant/1", token=token))
updated_restaurant_data = {
    "id": 1,
    "name": "Updated Restaurant",
    "description": "An even greater place to dine",
    "category": "Casual Dining",
    "hasDelivery": False,
    "contactEmail": "updated@restaurant.com",
    "contactName": "John Smith",
    "address": {
        "city": "Townsville",
        "street": "456 Elm St",
        "postalCode": "67-890"
    }
}
print(test_endpoint("PUT", "/Restaurant/1", updated_restaurant_data, token=token))
print(test_endpoint("DELETE", "/Restaurant/1", token=token))
print(test_endpoint("GET", "/Restaurant/admin/1", token=token))

print("All endpoints tested.")
