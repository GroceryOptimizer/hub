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
  
## API Reference  
   
### REST API Endpoints (Client Communication)  
#### `POST /api/cart`  
**Description:** Processes a shopping cart and returns a collection of store visits.  
**Request Body:**  
```json
{
  "cart": [
    {
      "name": "milk"
    },
    {
      "name": "ham"
    }
  ]
}
```  
**Response:**  
```json
[
  {
    "storeId": 2,
    "store": {
      "id": 2,
      "name": "Coop",
      "location": {
        "latitude": 40.3,
        "longitude": 32.1
      }
    },
    "stockItems": [
      {
        "product": {
          "name": "ham"
        },
        "price": 50
      }
    ]
  }
]
```  
**Error Responses:**  
- `400 Bad Request` if the cart is empty or missing.  

### gRPC Endpoints (Store Communication)  

#### `Products (InventoryRequest) → InventoryResponse`  
**Description:** Fetches product availability based on the shopping cart.  

**Request:**  
```proto
message InventoryRequest {
  repeated Product shoppingCart = 1;
}
```  

**Response:**  
```proto
message InventoryResponse {
  repeated StockItem stockItems = 1;
}
```  

#### `HandShake (HandShakeRequest) → HandShakeResponse`  
**Description:** Registers a store with the hub service.  

**Request:**  
```proto
message HandShakeRequest {
  Store store = 1;
}
```  

**Response:**  
```proto
message HandShakeResponse {
  string id = 1;
}
```  

#### `SendMessage (SendMessageRequest) → SendMessageResponse`  
**Description:** Sends a message to the store and receives a reply.  

**Request:**  
```proto
message SendMessageRequest {
  string message = 1;
}
```  

**Response:**  
```proto
message SendMessageResponse {
  string reply = 1;
}
```  

## Usage  
- Start within **30 seconds** after launching the Docker containers.  

## Contributors  
- [KikiBerg](https://github.com/KikiBerg)  
- [Raiu](https://github.com/Raiu)  
- [RikiRhen](https://github.com/RikiRhen)  
- [Syldriem](https://github.com/Syldriem)  
- [vikkoooo](https://github.com/vikkoooo)  

