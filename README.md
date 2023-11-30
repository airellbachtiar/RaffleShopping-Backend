# Raffle Shopping Backend

## Description
A raffle is a type of contest in which you buy a ticket for a chance of winning a prize. This concept will be brought into a shopping environment where a buyer/customer could sign-up to get a ticket for a shopping item that they wish to buy.

There will be a raffle organizer that can manage raffle event. They will be responsible for creating raffle events which a customer could participate in. Aside from raffle organizer, there will be a account manager to manage raffle organizer accounts. They will be responsible for creating and removing event organizer account. These 2 accounts are called manager account.

Customers can participate in a raffle event. They can apply for a ticket for a chance to buy an item from a raffle event.

## Third-Party Services
Azure Cloud Services:
    - Azure Service Bus (1)
    - Azure Cosmos DB for MongoDB (2)
    - Azure Storage Blob (1)
Firebase Authentication
(Prometheus and Grafana)

## Preparation
.env files are needed to be created to run this applications. .env files are located at and have content of:
1. RaffleShopping.Services.Catalogs.Api:
    - Catalog database connection string
    - Catalog database name
    - Catalog database collection name
    - Catalog service bus connection string
    - Catalog service bus queue name
    - Catalog blob storage connection string
    - Catalog blob storage container name
    - Catalog blob storage account name
2. RaffleShopping.Services.Customers.Api
    - Customer database connection string
    - Customer database name
    - Customer collection name
    - Firebase config path
3. RaffleShopping.Services.RaffleEvents.Api
    - Catalog service bus connection string
    - Catalog service bus name
4. Root Directory: All contents from previous .env files for Docker Compose

And firebase-config.json file is needed for firebase authentication credentials. This can be received from Firebase Authentication > Project Overview > Project Settings > Service Accounts > Generate New Private Key.

## How to Run
These services are recommended to be run using Kubernetes. Please have any Kubernetes cluster of your choice.

### Setup NGINX controller
Here are the commands to setup NGINX contoller. This action only needs to be performed once.
```bash
helm repo add nginx-stable https://helm.nginx.com/stable
helm repo update
helm install nginx-ingress nginx-stable/nginx-ingress --set rbac.create=true
```
After running the commands, nginx-ingress-controller should be running by runnning this command:
```bash
kubectl get services
```

### Applying Manifest
To apply Kubernetes, please use this command at the root directory.
```bash
kubectl apply -f ./.kubernetes
```
And make sure to add raffleshopping.com to localhost (127.0.0.1) by following these steps:
    - Press the Windows key. Type ‘Notepad’ in the search field. In the search results, right-click Notepad and select Run as administrator.
    - Press File > Open, then open the following file using this file path: c:\Windows\System32\Drivers\etc\hosts
    - If requested, open the file with Notepad.
    - Under: 127.0.0.1 localhost add 127.0.0.1 raffleshopping.com
    - Press File > Save to save your changes.
