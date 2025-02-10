# Grocery Optimizer - HUB  

## Description  
The **Grocery Optimizer - HUB** acts as middleware, facilitating communication between:  
- **GroceryOptimizer Client** via a **REST API**.  
- **GroceryOptimizer Store** via **gRPC**.  

This service ensures smooth data exchange and process coordination between the client and store systems.  

## Setup  
This project depends on:  
- **GroceryOptimizer Client**: [GitHub Repo](https://github.com/GroceryOptimizer/client)  
- **GroceryOptimizer Store**: [GitHub Repo](https://github.com/GroceryOptimizer/store)  

### Steps to Set Up (Visual Studio)  
1. Clone the repositories:  
   ```sh
   git clone https://github.com/GroceryOptimizer/hub.git  
   git clone https://github.com/GroceryOptimizer/client.git  
   git clone https://github.com/GroceryOptimizer/store.git  
   ```  
2. Open the **hub** project in **Visual Studio 2022**.  
3. Open the **Package Manager Console** and run database migrations:  
   ```sh
   Update-Database
   ```  
4. Set **Api** as the **Startup Item**.  
5. Select **HTTP** as the launch profile (dropdown next to the "Run" button in Visual Studio).  

### Steps to Set Up (Terminal Only)  
For those who prefer the terminal, follow these steps:  

1. Clone the repositories:  
   ```sh
   git clone https://github.com/GroceryOptimizer/hub.git  
   git clone https://github.com/GroceryOptimizer/client.git  
   git clone https://github.com/GroceryOptimizer/store.git  
   ```  
2. Navigate to the **hub** project directory:  
   ```sh
   cd hub
   ```  
3. Restore dependencies:  
   ```sh
   dotnet restore
   ```  
4. Run database migrations:  
   ```sh
   dotnet ef database update
   ```  
5. Build the project:  
   ```sh
   dotnet build
   ```  
6. Set the startup project to **Api** and launch with HTTP:  
   ```sh
   dotnet run --project Api --launch-profile "http"
   ```  
<details>
   <summary>Click to expand API Reference 🔴 **Work in Progress**  </summary>
   
## API Reference  🔴 **Work in Progress**  
⚠️ **Work in Progress:** These API endpoints are not finalized.
   
### REST API Endpoints (Client Communication)  
#### `GET /api/products`  
**Description:** Fetches a list of available products.  
**Request Parameters:** None  
**Response:**  
```json
[
  {
    "id": 1,
    "name": "Apples",
    "price": 2.99,
    "stock": 150
  },
  {
    "id": 2,
    "name": "Bananas",
    "price": 1.99,
    "stock": 200
  }
]
```  

#### `POST /api/order`  
**Description:** Places an order.  
**Request Body:**  
```json
{
  "customerId": 123,
  "items": [
    { "productId": 1, "quantity": 5 },
    { "productId": 2, "quantity": 3 }
  ]
}
```  
**Response:**  
```json
{
  "orderId": 456,
  "status": "Confirmed"
}
```  

### gRPC Endpoints (Store Communication)  

#### `CheckStock`  
**Description:** Checks stock availability for a product.  
**Request:**  
```proto
message StockRequest {
  int32 productId = 1;
}
```  
**Response:**  
```proto
message StockResponse {
  int32 productId = 1;
  int32 availableQuantity = 2;
}
```  

#### `ProcessOrder`  
**Description:** Processes an order request.  
**Request:**  
```proto
message OrderRequest {
  int32 orderId = 1;
}
```  
**Response:**  
```proto
message OrderResponse {
  int32 orderId = 1;
  string status = 2;
}
```  

   </details>    
       
## Usage  
- Start within **30 seconds** after launching the Docker containers.  

## Contributors  
- [KikiBerg](https://github.com/KikiBerg)  
- [Raiu](https://github.com/Raiu)  
- [RikiRhen](https://github.com/RikiRhen)  
- [Syldriem](https://github.com/Syldriem)  
- [vikkoooo](https://github.com/vikkoooo)  
