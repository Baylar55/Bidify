# Bidify

Bidify is an auction platform designed to connect buyers and sellers in real-time. The system consists of multiple microservices for scalability, with a modular architecture to support various functionalities like user management, auctions, bidding, and notifications.

## Architecture Overview

[![Bidify-Architecture.png](https://i.postimg.cc/DZYC0jTf/Bidify-Architecture.png)](https://postimg.cc/G8GPSjt6)

The architecture consists of the following main components:

### Client Applications
- **WebApp (NextJS)**: A web application using NextJS for server-side rendering and dynamic UI updates.

### Services
- **Identity Service**: Manages user authentication and authorization. Uses PostgreSQL as the database and includes security token services (STS).
- **Auction Service**: Handles auction creation, updates, and management. Uses a PostgreSQL database.
- **Search Service**: Provides efficient search functionality for auctions and users. Uses MongoDB for fast indexing and retrieval.
- **Bidding Service**: Manages the bidding process and stores bid information. Uses MongoDB as its database.
- **Notification Service**: Uses SignalR for real-time notifications, allowing users to receive instant updates on auction and bid statuses.

### Infrastructure
- **Event Bus**: Utilizes a Publish/Subscribe model to handle asynchronous communication between services.
- **Gateway**: Acts as the main entry point, handling requests and routing them to the appropriate services.
