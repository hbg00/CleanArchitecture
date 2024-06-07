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

### Testing
- Utilize the provided Python script in `ApiScript` folder to test endpoints.
- Ensure that any modifications to the seeded restaurant do not affect the script's functionality.
