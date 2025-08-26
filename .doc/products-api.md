[Back to README](../README.md)

### Products

#### GET /api/Products
- Description: Retrieve a list of all products
- Query Parameters:
  - `ActiveOnly` (optional): Filter to show only active products (boolean)
  - `Category` (optional): Filter by product category (string)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null,
    "data": {
      "products": [
        {
          "id": "uuid",
          "name": "string",
          "unitPrice": 0.0,
          "category": "string",
          "isActive": true,
          "createdAt": "2024-01-01T00:00:00Z"
        }
      ]
    }
  }
  ```

#### POST /api/Products
- Description: Create a new product
- Request Body:
  ```json
  {
    "name": "string",
    "description": "string",
    "unitPrice": 0.0,
    "category": "string",
    "isActive": true
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
      "name": "string",
      "description": "string",
      "unitPrice": 0.0,
      "category": "string",
      "isActive": true,
      "createdAt": "2024-01-01T00:00:00Z"
    }
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request data

#### GET /api/Products/{id}
- Description: Retrieve a specific product by ID
- Path Parameters:
  - `id`: Product ID (UUID format)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null,
    "data": {
      "id": "uuid",
      "name": "string",
      "description": "string",
      "unitPrice": 0.0,
      "category": "string",
      "isActive": true,
      "createdAt": "2024-01-01T00:00:00Z",
      "updatedAt": "2024-01-01T00:00:00Z"
    }
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request parameters
  - `404 Not Found`: Product not found

#### PUT /api/Products/{id}
- Description: Update a specific product
- Path Parameters:
  - `id`: Product ID (UUID format)
- Request Body:
  ```json
  {
    "name": "string",
    "description": "string",
    "unitPrice": 0.0,
    "category": "string",
    "isActive": true
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
      "name": "string",
      "description": "string",
      "unitPrice": 0.0,
      "category": "string",
      "isActive": true,
      "createdAt": "2024-01-01T00:00:00Z",
      "updatedAt": "2024-01-01T00:00:00Z"
    }
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request data

#### DELETE /api/Products/{id}
- Description: Delete a specific product
- Path Parameters:
  - `id`: Product ID (UUID format)
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
  - `404 Not Found`: Product not found

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./sales-api.md">Previous: Sales API</a>
  <a href="./carts-api.md">Next: Carts API</a>
</div>