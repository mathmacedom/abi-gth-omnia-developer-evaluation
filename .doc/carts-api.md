[Back to README](../README.md)

### Carts

#### GET /api/Carts/{id}
- Description: Retrieve a specific cart by ID
- Path Parameters:
  - `id`: Cart ID (UUID format)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null,
    "data": {
      "id": "uuid",
      "customerId": "uuid",
      "customerName": "string",
      "items": [
        {
          "id": "uuid",
          "productId": "uuid",
          "productName": "string",
          "unitPrice": 0.0,
          "quantity": 0,
          "subtotal": 0.0
        }
      ],
      "status": 1,
      "createdAt": "2024-01-01T00:00:00Z",
      "updatedAt": "2024-01-01T00:00:00Z"
    }
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request parameters
  - `404 Not Found`: Cart not found

#### GET /api/Carts/customer/{id}
- Description: Retrieve carts for a specific customer
- Path Parameters:
  - `id`: Customer ID (UUID format)
- Error Responses:
  - `400 Bad Request`: Invalid request parameters
  - `404 Not Found`: Customer not found

#### POST /api/Carts
- Description: Create a new cart
- Request Body:
  ```json
  {
    "branch": "string",
    "customerId": "uuid"
  }
  ```
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null,
    "data": {
      "id": "uuid",
      "branch": "string",
      "customerId": "uuid",
      "status": 1
    }
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request data

#### POST /api/Carts/{id}/checkout
- Description: Checkout a cart (complete the purchase)
- Path Parameters:
  - `id`: Cart ID (UUID format)
- Request Body:
  ```json
  {}
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request or cart cannot be checked out
  - `404 Not Found`: Cart not found

#### POST /api/Carts/{id}/items
- Description: Add an item to a cart
- Path Parameters:
  - `id`: Cart ID (UUID format)
- Request Body:
  ```json
  {
    "productId": "uuid",
    "quantity": 0
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request data
  - `404 Not Found`: Cart not found

#### PUT /api/Carts/{id}/items/{itemId}
- Description: Update quantity of a cart item
- Path Parameters:
  - `id`: Cart ID (UUID format)
  - `itemId`: Cart item ID (UUID format)
- Request Body:
  ```json
  {
    "quantity": 0
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request data

#### DELETE /api/Carts/{id}/items/{itemId}
- Description: Remove an item from a cart
- Path Parameters:
  - `id`: Cart ID (UUID format)
  - `itemId`: Cart item ID (UUID format)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request parameters
  - `404 Not Found`: Cart or item not found

#### Cart Status Values
- `1`: Active
- `2`: Converted
- `3`: Abandoned
- `4`: Expired
- `5`: PendingPayment
- `6`: Cancelled

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./products-api.md">Previous: Products API</a>
  <a href="./sales-api.md">Next: Sales API</a>
</div>