# SI_Assignment_4
We have developed two services in a microservice architecture. The two services communicate with each other with apache kafka. They will be developed further to gather data about the way users order, and and give a restaurant an option to reply to a user review/complaint.
## User Service
The user service handles user functionality such as logging in, creating a user and making a Review/complaint
## Feedback Service
The feedback service handles feedback functionality, and recieves a complaint from the userservice through kafka, which is then saved in the feedback database. Later on we will develop more functionality to do more cool stuff

We have developed this as a group of five, 3 people on user service, and 2 on feedback service

## How to run
- download visual studio 2022
- download docker desktop
- run the docker-compose file with ```docker-compose up-d```
- login with a user through postman on ```https://localhost:44367/connect/token```
- run endpoint ```https://localhost:44367/UserFeedback``` with body: ```{
  "userId": 1,
  "restaurantId": 1,
  "deliveryId": 1,
  "message": "Very nice food and delivery",
  "rating": 10
}```
- the review will now be saved in the feedback database. 



