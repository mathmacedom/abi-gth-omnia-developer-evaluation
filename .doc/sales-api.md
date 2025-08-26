[Back to README](../README.md)

### Sales

#### GET /api/Sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID (UUID format)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null,
    "data": {
      "id": "uuid",
      "customerId": "uuid",
      "branch": "string",
      "totalAmount": 0.0,
      "totalDiscount": 0.0,
      "isCancelled": false,
      "createdAt": "2024-01-01T00:00:00Z",
      "updatedAt": "2024-01-01T00:00:00Z",
      "cancelledAt": null,
      "items": [
        {
          "productId": "uuid",
          "productName": "string",
          "quantity": 0,
          "unitPrice": 0.0,
          "discount": 0.0,
          "total": 0.0,
          "isCancelled": false
        }
      ]
    }
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request parameters
  - `404 Not Found`: Sale not found

#### PATCH /api/Sales/{id}/cancel
- Description: Cancel an entire sale
- Path Parameters:
  - `id`: Sale ID (UUID format)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request or sale cannot be cancelled
  - `404 Not Found`: Sale not found

#### PATCH /api/Sales/{id}/items/{itemId}/cancel
- Description: Cancel a specific item within a sale
- Path Parameters:
  - `id`: Sale ID (UUID format)
  - `itemId`: Sale item ID (UUID format)
- Response:
  ```json
  {
    "success": true,
    "message": "string",
    "errors": null
  }
  ```
- Error Responses:
  - `400 Bad Request`: Invalid request or item cannot be cancelled
  - `404 Not Found`: Sale or item not found

#### Sale Item Properties
- `productId`: Unique identifier of the product
- `productName`: Name of the product at time of sale
- `quantity`: Number of units sold
- `unitPrice`: Price per unit at time of sale
- `discount`: Discount amount applied to this item
- `total`: Final amount for this item (unitPrice * quantity - discount)
- `isCancelled`: Whether this specific item has been cancelled

#### Sale Properties
- `customerId`: ID of the customer who made the purchase
- `branch`: Branch/location where the sale occurred
- `totalAmount`: Total sale amount before discounts
- `totalDiscount`: Total discount amount applied to the sale
- `isCancelled`: Whether the entire sale has been cancelled
- `createdAt`: When the sale was created
- `updatedAt`: When the sale was last modified
- `cancelledAt`: When the sale was cancelled (null if not cancelled)

#### Sale Status Values
- `1`: Open
- `2`: Completed
- `3`: Cancelled

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./users-api.md">Next: Users API</a>
</div>