# CleanArchitecture Restaurant App

## Setup Instructions

### Pre-requisites
- Two databases should be created: one for the restaurant and one for identity.

### Configuration Setup

1. **Restaurant API Configuration:**
   - Open `RestaurantAPI/appsettings.Development.json`.
   - Add the restaurant database connection string as `"Db"`.
   - Add the identity database connection string as `"DbIdentity"`.

2. **gRPC Configuration:**
   - Open `gRPC/appsettings.Development.json`.
   - Add the identity database connection string under `"gRPCIdentityServer"`.

3. **Entity Framework Migrations:**
   - Open Package Manager Console.
   - Run the following commands:
     ```shell
     Add-Migration init -Project Infrastructure -Context RestaurantDbContext
     Update-Database -Context RestaurantDbContext
     Add-Migration init -Project Identity -Context IdentityDbContext
     Update-Database -Context IdentityDbContext
     ```
## Starting the Application

1. **Configure Startup Project:**
   - Enter into Visual Studio.
   - Choose `Configure Startup Projects` from the `Debug` menu.

2. **Select Multiple Startup Projects:**
   - Choose `Multiple Startup Projects` from the configuration options.

3. **Configure Startup Actions:**
   - For the `start` action, set the following projects:
     - `RestaurantAPI`
     - `RestaurantUI`
     - `GrpcService`

4. **Run the Application:**
   - Click `Start` to run.
     
## Testing

### Unit Tests

- **XUnit Tests:**
  - There are unit tests available in XUnit for:
    - `ApplicationLayer`
    - `DomainLayer`

### API Tests

- **Python Script:**
  - A Python script is provided for testing the API.
  - Ensure that migrations have been applied before running the script.
  - Note: The script only works without changing any data provided by migration.
