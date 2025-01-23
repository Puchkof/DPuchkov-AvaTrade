# DPuchkov-AvaTrade
Solution for AvaTrade test task by Dmytro Puchkov

## Task 1: Architecture Solution

### Solution Comparison

| Aspect | Solution 1 (Microservice) | Solution 2 (Monolith) |
|--------|--------------------------|---------------------|
| Scalability | High - Independent scaling of news processor and API services | Medium - Vertical scaling of single application |
| Complexity | Higher - Multiple services with own databases and message queues | Lower - Single codebase and database |
| Deployment | Container-based with Kubernetes orchestration | Single deployment to App Service |
| Development Speed | Slower initial - Multiple services setup required | Faster initial - Single solution setup |
| Maintenance | More complex - Multiple deployment points and monitoring | Simpler - Single deployment and monitoring point |
| Infrastructure | Multiple databases, message queues, and services | Single database and application instance |
| Cost | Higher - Multiple resources and orchestration | Lower - Single application resources |
| Team Structure | Multiple teams can work independently | Single team with shared codebase |


### Recommended Solution

After analyzing the requirements and comparing different approaches, the **Microservice Architecture** is recommended with the following structure:

### Component Responsibilities

- **AvaTrade.News.API**: REST endpoints for news retrieval, subscription management, and API documentation using Swagger
- **AvaTrade.News.Application**: Business logic implementation, news processing services, caching strategies, and DTOs
- **AvaTrade.News.Domain**: Core business entities (NewsItem, Subscription), interfaces, and domain-specific rules
- **AvaTrade.News.Infrastructure**: Integration with Polygon.io, database repositories, and external service implementations
- **AvaTrade.News.Shared**: Common utilities, constants, and shared models across all layers
- **AvaTrade.News.Processor**: Background service for fetching, processing, and enriching news data from external providers

#### Architecture Components

1. **News API Service (Query Side)**
   - Handles all read operations
   - Uses dedicated read database
   - Implements caching for performance
   - Provides REST endpoints for news retrieval

2. **News Processor Service (Command Side)**
   - Fetches news from external providers
   - Processes and enriches news data
   - Writes to command database
   - Publishes events for synchronization

3. **Data Flow & Consistency**
   - Write Path: `Provider -> Processor -> Command DB -> Event Bus`
   - Read Path: `Client -> API -> Cache/Query DB`
   - Sync Path: `Event Bus -> Query DB Update`

The solution will be implemented using .NET 8 with the following structure:

```plaintext
AvaTrade.News.Solution/
├── src/
│   ├── AvaTrade.News.API         # Web API project
│   ├── AvaTrade.News.Application # Application logic, DTOs, interfaces
│   ├── AvaTrade.News.Domain      # Domain entities and logic
│   ├── AvaTrade.News.Infrastructure # External services, database, etc.
│   ├── AvaTrade.News.Processor   # News processing background service
│   └── AvaTrade.News.Shared      # Shared utilities and constants
└── tests/
    ├── AvaTrade.News.UnitTests
    └── AvaTrade.News.IntegrationTests
```

This architecture provides:
- Clear separation of read and write operations
- Independent scaling of services
- Optimized data models for different use cases
- Event-driven synchronization with eventual consistency
- Clean architecture principles for maintainability

### Task List and Estimations

#### 1. Project Setup and Infrastructure (2-3 days)
- Solution structure setup with all projects
- Database design and implementation
- Message queue setup
- Basic CI/CD configuration

#### 2. News Processor Service (4-5 days)
- Polygon.io integration
- News fetching and processing logic
- Command database operations
- Event publishing implementation
- Error handling and retry logic
- Unit tests

#### 3. News API Service (3-4 days)
- REST API endpoints implementation
- Query database operations
- Caching layer
- Authorization/Authentication
- API documentation
- Unit tests

#### 4. Data Synchronization (2-3 days)
- Event handling implementation
- Read database updates
- Consistency monitoring
- Error handling
- Integration tests

#### 5. Testing and Documentation (2-3 days)
- Integration testing
- Performance testing
- API documentation finalization
- Deployment documentation
- Knowledge transfer documentation

Total Estimation: 13-18 days

User Stories:
1. As a Product Owner, I want to fetch news from Polygon.io every hour so that I can keep the news database up to date
2. As a Product Owner, I want to enrich news data with instrument information so that users can get more valuable insights
3. As an AvaTrade User, I want to get all news items so that I can display them in my application
4. As an AvaTrade User, I want to get news for a specific instrument so that I can show relevant news to traders
5. As an AvaTrade User, I want to subscribe to news updates so that I can receive notifications about new news items
6. As a Public User, I want to get the latest news for top instruments so that I can make trading decisions