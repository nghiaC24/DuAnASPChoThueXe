# Database Rebuild Instructions

## Overview
This document provides instructions to rebuild the KNPRentalDB database using the SQL script created from your original database export.

## Files Created
- `Rebuild_KNPRentalDB.sql` - Complete database recreation script
- `Database_Rebuild_Instructions.md` - This instruction file

## Database Structure
The script recreates the following tables exactly as in your original database:

1. **__EFMigrationsHistory** - Entity Framework migrations tracking
2. **Categories** - Motorbike categories (Id, Name, Description)
3. **Motorbikes** - Motorbike inventory (Id, Name, CategoryId, Capacity, PricePerDay, Description, ImageUrl, Suggestion)
4. **Users** - User accounts (Id, FullName, Email, Password, Role)
5. **CartItems** - Shopping cart items (Id, MotorbikeId, Quantity, Price, StartDate, EndDate, CartId)
6. **Bookings** - Rental bookings (Id, CustomerName, CustomerPhone, StartDate, EndDate, TotalPrice, Status, MotorbikeId)
7. **FAQs** - Frequently asked questions (Id, Question, Answer)

## Foreign Key Relationships
- Motorbikes.CategoryId → Categories.Id (CASCADE DELETE)
- CartItems.MotorbikeId → Motorbikes.Id (CASCADE DELETE)
- Bookings.MotorbikeId → Motorbikes.Id (CASCADE DELETE)

## How to Run the Script

### Option 1: SQL Server Management Studio (SSMS)
1. Open SSMS
2. Connect to your SQL Server instance
3. Open a new query window
4. Open the `Rebuild_KNPRentalDB.sql` file
5. Execute the script (F5 or click Execute)

### Option 2: SQL Server Command Line
```bash
sqlcmd -S your_server_name -i "d:\ChoThueXe.ASP\DuAnASPChoThueXe\Rebuild_KNPRentalDB.sql"
```

### Option 3: Visual Studio
1. Open Visual Studio
2. Go to SQL Server Object Explorer
3. Right-click on your SQL Server instance
4. Select "New Query"
5. Open and execute the `Rebuild_KNPRentalDB.sql` file

## Important Notes
- The script will create the database if it doesn't exist
- If the database already exists, you may need to drop it first or modify the script
- All tables are created with the exact same structure as your original export
- Foreign key constraints are added after table creation
- The script includes all original settings (ANSI_NULLS, QUOTED_IDENTIFIER, etc.)

## Verification
After running the script, you can verify the database was created correctly by:
1. Checking that all 7 tables exist
2. Verifying foreign key relationships
3. Confirming that your ASP.NET application can connect to the database

## Next Steps
Once the database is rebuilt, you may need to:
1. Add initial data (categories, motorbikes, etc.)
2. Update your application's connection string if needed
3. Test your application functionality
