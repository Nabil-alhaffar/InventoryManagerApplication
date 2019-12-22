# InventoryManagerApplication
This is a project developed in order to do a better job tracking inventory in a phone on the go, without needing to refer to
a desktop.
This project consists of three parts: 
1) iOS Xamarin Application: This client application is used in order to manage inventory in a Metro by T-mobile franchise. 
 This application will allow district managers to perform multiple tasks related to managing inventory including but not 
 restricted to receiving orders, adding/subtracting stocks, shipping stocks to a different locations, counting stocks
 and reconciliation. It will communicate with an AzureSQL database using an ASP.NET Mobile app service. In addition, it will 
 uses OAuth 2.0 in order to authenticate and authorize users for different functions. 

2) ASP.NET Web API: This application will use HTTP Clients in order to communicate with the AzureSQL Database. This app is
  hosted on Azure, and exchanges information with the database using Add/get/delete/update requests. The UI consists of 
  Swashbuckle's Swagger interface (Third Party), which provides great means of testing the API functions. 
  
3) Database: In order to store the information used by this application, an SQL database has been created with multiple tables 
to store the needed data. 




