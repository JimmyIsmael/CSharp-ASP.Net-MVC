# Online Cigar Store

This project contains a online cigar store with sign-up, loging, ordering, cart, orders, and product management. 

## To Run This Project
Restore the application database on your local instance of Microsoft SQL Server. Use the CigarStoreDB.bak file to do so. 
Go to the Web.config file of the project and edit the connectionString property with the connection string to the database. 
Open the project in Visual Studio and download all the packages needed. 
Run the project. 

There are two users on the database. One is called demo user and the other demo admin. The admin user has access to the administrative tasks of the app, such as adding items, reviewing orders and activating new users. 
The demo user is a regular user that signs up to the app to use the online store. 

## App Functionalities

### Regular Users
* Users can sign up for the app.
* Log in and log out.
* Add products to cart. 
* Checkout new orders.
* Review and print orders. 

### Admin Users
* Activate new regular users that sign up to the app. 
* Edit existing users.
* Add products.
* Edit products.
* Revew orders.
* Ship orders. 

## Project Screenshots

### Login
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/Login.PNG)

### Store
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/Store.PNG)

### Product
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/Product.PNG)

### Cart Preview
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/StoreCart.PNG)


### Cart
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/Cart.PNG)

### Checkout
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/Checkout.PNG)

### Order Confirmation
![Screenshot](https://github.com/JimmyIsmael/CSharp-ASP.Net-MVC/blob/master/CigarStoreProject/OrderConfirmation.PNG)

