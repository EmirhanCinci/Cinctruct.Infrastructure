<h1>Cinctruct NuGet Package</h1>

<h3 align="center">
    <img src="https://readme-typing-svg.herokuapp.com/?font=Righteous&size=35&center=true&vCenter=true&width=500&height=70&duration=4000&lines=Comprehensive+Solutions+for;+Modern+Software,;+All+in+One+Package!;" />
</h3>

<h2>Overview</h2>

<p>
This NuGet package provides a comprehensive infrastructure for modern software development. It includes features such as generic systems, AOP (Aspect-Oriented Programming) supported validation and performance monitoring, custom error handling, extension methods, email sending, file operations, encryption, and API response generalization.
</p>

<h2>Features</h2>

- **Entity and DTO Abstraction**: Basic entity and DTO classes for the model layer.
- **Data Access**: Abstraction for generic data access methods.
- **AOP (Aspect-Oriented Programming)**: Aspects such as validation controls and performance monitoring.
- **Error Handling**: Custom error classes and middleware.
- **Extension Methods**: Extension methods for structures String, DateTime, Claim, and ClaimPrincipal.
- **Email and File Operations**: General email sending and file reading/writing operations.
- **Encryption**: Encryption structures for password operations.
- **API Responses**: Configurations for generalizing API responses.

<h2>Installation</h2>
 
You can install the package from NuGet using the following command:

``` bash
dotnet add package Cinctruct.Infrastructure
```

<h2>Usage</h2>

### 1. Entity and DTO Usage

We provide some examples to help you on how to structure your model layer with this NuGet package. Below are examples of how Entity and DTO classes can be used:

#### 1.1. Entity Usage

**`Entity`** classes provide the fundamental structures representing your database tables. Typically, you derive these classes from **`BaseEntity`**. Here’s an example of how to define a **`User`** entity:

``` C#
public class User : BaseEntity<long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
```

**Explanation:**

* The **`User`** class derives from **`BaseEntity<long>`**, providing it with essential properties such as **`Id`**, **`CreatedDate`**, **`UpdatedDate`**, **`DeletedDate`**, and **`IsDeleted`**.

* The **`FirstName`** and **`LastName`** properties represent the user's first and last names, respectively. These properties are initialized with empty strings by default.

#### 1.2. DTO Usage

**`DTO`** (Data Transfer Object) classes facilitate the transfer and transport of data. Here’s an example of how to create a DTO derived from **`BaseDto`**:

``` C#
public class UserDto : BaseDto<long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
```

**Explanation:**

* The **`UserDto`** class derives from **`BaseDto<long>`**, allowing it to include properties such as **`Id`**, **`CreatedDate`**, **`UpdatedDate`**, **`DeletedDate`**, and **`IsDeleted`**.

* The **`FirstName`** and **`LastName`** properties represent the user's first and last names, and they are used for data transfer.

<br/>

### 2. Data Access

#### 2.1. Repository Pattern Usage

The repository pattern makes your code more organized and manageable by separating data access code from application code. This NuGet package allows you to abstract and extend data access using the repository pattern.

**`IUserRepository`** is an interface used for interacting with user data. It derives from **`IBaseRepository`** and provides basic CRUD (Create, Read, Update, Delete) operations. Here is the definition of the **`IUserRepository`** interface:

``` C#
public interface IUserRepository : IBaseRepository<User, long>
{
    // You can add custom methods specific to this repository
}
```

**Explanation:**

* **`IUserRepository`** derives from **`IBaseRepository<User, long>`** and contains data access methods for the **`User`** entity with **`long`** type IDs.

* This interface allows you to add custom methods specific to user data in your data access layer.

<br/>

**`UserRepository`** is a repository class derived from **`EfBaseRepository`** and implements the **`IUserRepository`** interface. This class can provide custom data access methods for interacting with user data. Here is the definition of the **`UserRepository`** class:

``` C#
public class UserRepository : EfBaseRepository<User, long, AppDbContext>, IUserRepository
{
    // Custom repository methods can be defined here
}
```

**Explanation:**

* **`UserRepository`** derives from **`EfBaseRepository<User, long, AppDbContext>`**, performing data access operations for the **`User`** entity with **`long`** type IDs and using the **`AppDbContext`** context.

* **`EfBaseRepository`** provides data access using Entity Framework Core and includes basic CRUD operations.

* The **`UserRepository`** class implements the **`IUserRepository`** interface, allowing you to add custom data access methods specific to user data.

<br/>

These examples demonstrate how to define and implement repositories for effective data access and manipulation in your application.

#### 2.2. UnitOfWork Pattern Usage

The **`UnitOfWork`** pattern coordinates the work of multiple repositories by managing the database transactions. It ensures that all changes made within a transaction are committed together, maintaining data consistency and integrity.

Below is an example implementation of a service using the **`UnitOfWork`** pattern:

``` C#
public class UserService : IUserService 
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    public UserService(IUserRepository userRepository, IUnitOfWork<AppDbContext> unitOfWork) 
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomApiResponse<UserDto.UserGetDto>> AddAsync(UserDto.UserPostDto dto) 
    {
        // Map the UserPostDto to the User entity
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName
            // Map other properties as necessary
        };
        // Add the new user entity to the repository
        var insertedUser = await _userRepository.AddAsync(user);
        // Commit the transaction to persist changes to the database
        await _unitOfWork.CommitAsync();
        // Map the inserted user entity to the UserGetDto and return a response
        var userGetDto = new UserDto.UserGetDto
        {
            Id = insertedUser.Id,
            FirstName = insertedUser.FirstName,
            LastName = insertedUser.LastName
            // Map other properties as necessary
        };
        return CustomApiResponse<UserDto.UserGetDto>.Success(StatusCodes.Status201Created, userGetDto);
    }
}
```

**Explanation:**

**`UserService`** is a service class implementing the **`IUserService`** interface, managing user-related operations using **`IUserRepository`** and **`IUnitOfWork`**.

* The **`AddAsync`** method demonstrates how to add a new user:
    * First, the DTO (**`UserDto.UserPostDto`**) is mapped to a corresponding **`User`** entity.
    * The **`AddAsync`** method of the **`IUserRepository`** is used to insert the new user into the database context.
    * The **`CommitAsync`** method of the **`IUnitOfWork`** is called to save the changes, ensuring that the insertion is committed as part of a transaction.
    * Finally, the inserted entity is mapped to a **`UserDto.UserGetDto`** and returned in a custom API response.
* The UnitOfWork pattern in this example coordinates the commit of changes made by the repository, ensuring that all changes are completed successfully, and manages transactions effectively.

This approach encapsulates the transaction logic within the service layer, providing a clean and organized method for handling data operations across multiple repositories.

<br/>

### 3. AOP (Aspect-Oriented Programming)

Aspect-Oriented Programming (AOP) allows for the separation of cross-cutting concerns (such as logging, validation, performance monitoring) from the main business logic, making your code cleaner, more modular, and easier to maintain. In this package, various aspects can be applied to your service classes and methods to handle concerns like performance monitoring, validation, and null checks.

Below is an example of how AOP is implemented in the **`UserService`** class using different aspects:

``` C#
[PerformanceAspect(5)]
public class UserService : IUserService 
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    public UserService(IUserRepository userRepository, IUnitOfWork<AppDbContext> unitOfWork) 
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    [DtoNullCheckAspect]
    [ValidationAspect(typeof(UserDtoValidator.UserPostDtoValidator))]
    public async Task<CustomApiResponse<UserDto.UserGetDto>> AddAsync(UserDto.UserPostDto dto) 
    {
        
    }

    [IdCheckAspect]
    public async Task<CustomApiResponse<UserDto.UserGetDto>> GetById(long id) 
    {
        
    }
}
```

**Explanation of Aspects Used:**

1. **`PerformanceAspect(5):`**

This aspect is applied to the **`UserService`** class level and monitors the performance of all methods within the class. The parameter **`5`** indicates the threshold in seconds; if a method execution exceeds this time, it might trigger a performance warning or logging action.

2. **`DtoNullCheckAspect:`**

Applied to the **`AddAsync`** method, this aspect checks if the DTO (Data Transfer Object) parameter is null before executing the method. If the DTO is null, it will prevent the method execution, potentially throwing an exception or returning an error response.
ValidationAspect(typeof(UserDtoValidator.UserPostDtoValidator)):

3. **`ValidationAspect(typeof(UserDtoValidator.UserPostDtoValidator)):`**

Also applied to the **`AddAsync`** method, this aspect performs validation on the DTO using a specified validator class (**`UserDtoValidator.UserPostDtoValidator`**). It ensures that the input data adheres to defined validation rules (e.g., required fields, data format). If validation fails, the method execution is stopped, and a validation error response is generated.
IdCheckAspect:

4. **`IdCheckAspect:`**

This aspect is applied to the **`GetById`** method and checks if the provided ID is valid (e.g., not zero or negative). It helps prevent unnecessary database queries or method executions if the ID is not valid.

**Benefits of Using AOP:**

* **Code Cleanliness and Separation of Concerns:** AOP keeps cross-cutting concerns out of the main business logic, making the code more readable and maintainable.
* **Reusability:** Aspects can be reused across different services and methods, reducing code duplication.
* **Ease of Maintenance:** Changes to cross-cutting concerns (like validation rules or performance thresholds) can be made in a centralized place, rather than throughout the codebase.
* **Enhanced Modularity:** By applying aspects, individual concerns are encapsulated in their respective aspects, enhancing the modularity of the application.

Using AOP, the **`UserService`** class is enhanced with performance monitoring, DTO null checks, validation, and ID checks, all without embedding these concerns directly in the business logic, thereby maintaining a clean separation of responsibilities.

<br/>

### 4. Error Handling

Error handling is a critical aspect of any application, as it ensures that unexpected conditions are managed gracefully and meaningful responses are provided to the client. In this package, a structured approach to error handling is implemented using custom exception classes and middleware.

#### 4.1. NoContentException

**Purpose:** Thrown when an operation completes successfully but does not return any content, aligning with HTTP status code 204 (No Content).

**Usage:** Use this exception when you want to indicate that the request was processed successfully, but there's no content to return to the client.

``` C#
public class NoContentException : Exception
{
    public NoContentException(string message) : base(message)
    {
        
    }
}
```

#### 4.2. BadRequestException

**Purpose:** Represents errors that occur due to invalid client input, corresponding to HTTP status code 400 (Bad Request).

**Usage:** Use this exception when validating inputs or handling scenarios where the client sends incorrect data that doesn't conform to expected formats or constraints.

``` C#
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {

    }
}
```

#### 4.3. BusinessRuleException

**Purpose:** Thrown when a business rule is violated, also resulting in a 400 status code.

**Usage:** This exception is used when the application logic detects a rule breach that must be communicated to the client, such as a constraint violation in the business domain.

``` C#
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message)
    {

    }
}
```

#### 4.4. NotFoundException

**Purpose:** Used when a requested resource is not found, corresponding to HTTP status code 404 (Not Found).

**Usage:** Use this exception to signal that an entity, such as a user or product, could not be found in the database or other data source.

``` C#
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {

    }
}
```

#### 4.5. CustomExceptionHandler 

The **`CustomExceptionHandler`** middleware centralizes error handling within the application. It captures exceptions thrown during request processing and translates them into appropriate HTTP responses.

Here's the implementation:

``` C#
public static class CustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                List<string> errorMessages = new List<string>();
                var statusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                switch (exceptionFeature?.Error)
                {
                    case NoContentException:
                        statusCode = StatusCodes.Status204NoContent;
                        errorMessages.Add(exceptionFeature.Error.Message);
                        break;
                    case ArgumentException:
                        statusCode = StatusCodes.Status400BadRequest;
                        errorMessages.Add(exceptionFeature.Error.Message);
                        break;
                    case BadRequestException:
                        statusCode = StatusCodes.Status400BadRequest;
                        errorMessages.Add(exceptionFeature.Error.Message);
                        break;
                    case BusinessRuleException:
                        statusCode = StatusCodes.Status400BadRequest;
                        errorMessages.Add(exceptionFeature.Error.Message);
                        break;
                    case ValidationException:
                        statusCode = StatusCodes.Status400BadRequest;
                        var errors = exceptionFeature.Error.Message.Split("-");
                        foreach (var error in errors)
                        {
                            errorMessages.Add(error.Trim());
                        }
                        break;
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        errorMessages.Add(exceptionFeature.Error.Message);
                        break;
                    case InvalidOperationException:
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        errorMessages.Add(exceptionFeature.Error.Message);
                        break;
                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        errorMessages.Add(SystemMessages.InternalServerError);
                        break;
                }
                context.Response.StatusCode = statusCode;
                var response = CustomApiResponse<NoData>.Fail(statusCode, errorMessages);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });
    }
}
```

**Explanation:**

Exception Handling Pipeline: The middleware sets up an exception handling pipeline using **`app.UseCustomException();`**, capturing exceptions globally across the application.

**Exception Mapping:**

* **NoContentException:** Returns a 204 status code with no content when this exception is caught.
* **BadRequest Scenarios:** Includes ArgumentException, BadRequestException, BusinessRuleException, and ValidationException, all mapped to a 400 status code. For validation errors, the messages are split and added individually for detailed feedback.
* **NotFoundException:** Mapped to a 404 status code, indicating the requested resource was not found.
* **InvalidOperationException:** Mapped to a 422 status code, used for unprocessable entity scenarios where the server understands the request but can't process it due to logical errors.
* **Default Case:** Captures all other exceptions and returns a 500 status code, indicating a server error.

**Response Formatting:**

* The middleware sets the response content type to JSON and assigns the appropriate status code based on the exception type.
* It constructs a failure response using **`CustomApiResponse<NoData>.Fail()`**, embedding the status code and error messages.
* The response is serialized to JSON and sent back to the client, ensuring a consistent and informative error response format.

**Benefits of Using Custom Exception Handling:**

* **Centralized Error Management:** Handles exceptions in one place, reducing redundancy and improving code maintainability.
* **Clear Client Communication:** Provides clients with clear, standardized error messages and HTTP status codes, enhancing the overall user experience.
* **Modular and Extensible:** Easily extendable to include additional exception types or customize the response formatting as needed.

This setup ensures that your application can manage various error scenarios in a robust, scalable manner, delivering clear and consistent feedback to clients.

<br/>

### 5. Extension Methods

Extension methods are a powerful feature in C# that allow you to add new functionality to existing types without modifying their source code or creating new derived types. They provide a clean and modular way to enhance the capabilities of existing types, making your code more readable and maintainable. In this project, several extension methods have been implemented to streamline common operations across various types.

#### 5.1. Claim Extensions

The **`ClaimExtensions`** class adds convenience methods for adding common claims to a collection of **`Claim`** objects. These methods simplify the process of adding frequently used claims such as email, name, name identifier, and roles.

* **AddEmail:** Adds a claim with the type **JwtRegisteredClaimNames.Email** and the provided email value.
* **AddName:** Adds a claim with the type **ClaimTypes.Name** and the provided name value.
* **AddNameIdentifier:** Adds a claim with the type **ClaimTypes.NameIdentifier** and the provided identifier value.
* **AddRoles:** Adds multiple role claims from an array of strings to the claims collection.

These methods help ensure consistent claim management and reduce boilerplate code when working with claims.

#### 5.2. ClaimsPrincipal Extensions

The **`ClaimsPrincipalExtensions`** class provides methods to easily retrieve specific claim values from a **`ClaimsPrincipal`** object.

* **GetEmail:** Retrieves the email claim value from the principal.
* **GetName:** Retrieves the name claim value from the principal.
* **GetNameIdentifier:** Retrieves the name identifier claim value from the principal.
* **GetRoles:** Retrieves all role claim values as an array of strings.

These extensions improve code readability and make it straightforward to access claim information in an authentication or authorization context.

#### 5.3. DateTime Extensions

The **`DateTimeExtensions`** class introduces helpful methods for working with **`DateTime`** objects.

* **IsWeekend:** Checks if the date falls on a weekend (Saturday or Sunday).
* **IsWeekday:** Checks if the date falls on a weekday (Monday to Friday).
* **CalculateAge:** Calculates the age based on the provided birthdate. This is useful for age-related validations or calculations.

These methods enhance the functionality of DateTime by providing intuitive ways to assess date characteristics and perform common date-related calculations.

#### 5.4. IQueryable Paginate Extensions

The **`IQueryablePaginateExtensions`** class provides methods to paginate query results, which is essential for handling large datasets in a manageable way.

* **ToPaginateAsync:** Asynchronously paginates an **`IQueryable<T>`** source based on the specified page index and size, returning a **`Paginate<T>`** object with the results.
* **ToPaginate:** Synchronously paginates an **`IQueryable<T>`** source in a similar manner.

These extensions support efficient data handling in applications by providing a structured way to split large result sets into pages, enhancing performance and user experience.

#### 5.5. String Extensions

The **`StringExtensions`** class includes a wide range of methods to perform common string manipulations and validations.

**Validation Methods:**
* **`IsValidUrl:`** Checks if a string is a valid URL format.
* **`IsValidEmail:`** Validates if a string follows the standard email format.
* **`IsNumeric:`** Checks if the string consists only of digits.

**Content Checks:**
* **`ContainUpperCase:`** Checks if the string contains uppercase letters.
* **`ContainLowerCase:`** Checks if the string contains lowercase letters.
* **`ContainDigit:`** Checks if the string contains any digits.
* **`ContainSpecialCharacter:`** Checks if the string contains special characters.

**String Manipulations:**
* **`TrimAndReduce:`** Trims the string and reduces multiple spaces to a single space.
* **`ToTitleCase:`** Converts the string to title case, capitalizing the first letter of each word.
* **`Truncate:`** Truncates the string to a specified length and appends "..." if needed.
* **`RemoveSpecialCharacters:`** Removes all characters except letters, digits, underscores, and periods.
* **`EncodeHtml / DecodeHtml:`** Encodes and decodes HTML special characters.
* **`ToSlug:`** Converts the string to a URL-friendly slug format.
* **`Base64Encode / Base64Decode:`** Encodes and decodes the string to and from Base64.
* **`EnsureStartsWith / EnsureEndsWith:`** Ensures the string starts or ends with a specified prefix or suffix.
* **`Mask:`** Masks part of the string, showing only the specified number of characters at the start and end, with the rest replaced by a masking character.

These string extensions provide robust functionality for processing, validating, and transforming strings in various scenarios, making your code cleaner and reducing repetitive logic.

These extension methods collectively enhance the functionality of various data types, providing more intuitive and readable ways to perform common operations. By leveraging these methods, you can write cleaner, more maintainable, and efficient code throughout your application.

<br/>

### 6. Email and File Operations

In many applications, handling file storage and email communication are critical tasks. The **`FileHelper`** and **`EmailService`** classes in this project are designed to provide robust solutions for these needs, integrating easily with the application's configuration settings.

#### 6.1. File Operations with **`FileHelper`**

The **`FileHelper`** class provides functionality to save files uploaded to the system. It handles the conversion from base64 encoded strings, unique file naming, directory creation, and returning the accessible URL of the saved file.

**`SaveFile(FileDto fileDto, string mainFilePath):`**
* **Purpose:** Saves a file represented by a **`FileDto`** object to a specified path on the server.
* **Process:**
    * Converts the base64 string from the **`FileDto`** to a byte array.
    * Extracts the file extension and generates a unique file name using a GUID to avoid name collisions.
    * Combines the file path and ensures the directory exists, creating it if necessary.
    * Writes the byte array to the file system at the specified location.
    * Constructs the full URL of the file using the base URL from configuration settings, making it accessible from outside the server.
* **Usage:** This method is useful in scenarios where files need to be uploaded, saved, and served, such as user profile pictures, document uploads, or any other file storage needs.

This approach ensures files are saved in a structured and organized manner, and URLs are consistently formatted, making them easy to access and manage.

#### 6.2. Email Operations with **`EmailService`**

The **`EmailService`** class is designed to handle sending emails, supporting both single and batch email sending operations. It utilizes SMTP (Simple Mail Transfer Protocol) to send emails based on configuration settings defined in the application's configuration file.

**`SendEmailAsync(EmailDto.EmailPostArrayDto dto):`**

* **Purpose:** Sends emails to multiple recipients as defined in the **`EmailPostArrayDto`**.
* **Process:**
    * Retrieves email configuration settings, such as the SMTP host, port, sender email, and password, from the application's configuration file.
    * Iterates over the list of recipient email addresses and sends an email to each.
    * Configures the SMTP client and constructs the email message for each recipient.
    * Catches and records any exceptions, allowing for robust error handling and reporting back which emails failed and why.
* **Usage:** Ideal for scenarios where bulk emails need to be sent, such as newsletters, notifications, or alerts to multiple users.

**`SendEmailAsync(EmailDto.EmailPostDto dto):`**

* **Purpose:** Sends a single email to a recipient defined in the EmailPostDto.
* **Process:**
    * Similar to the batch send method, it retrieves email settings, sets up the SMTP client, and sends the email.
    * Handles exceptions to ensure that the application can gracefully handle errors and provide feedback on failed email attempts.
* **Usage:** Useful for one-on-one communications such as account confirmations, password resets, or personal notifications.

**`Error Handling:`** Both methods in EmailService include comprehensive error handling by catching exceptions thrown during the email sending process. This allows the service to provide detailed feedback on what went wrong, which is critical for troubleshooting and ensuring reliable communication with users.

**`Configuration Integration:`** The integration with IConfiguration allows these services to be flexible and easily adaptable to different environments (development, staging, production) without changing the codebase. Configuration settings like email server details or file base URL can be adjusted directly in the configuration files.

These classes together provide essential functionality for managing file uploads and email communications in your application, ensuring a smooth user experience and robust operational capabilities. The use of configuration settings makes them flexible and adaptable to various deployment environments, which is a best practice for maintainable and scalable application design.

<br/>

### 7. Encryption

Encryption is a crucial component of application security, especially when dealing with sensitive data such as passwords, tokens, and other personal information. The classes provided in this section offer robust mechanisms for creating and managing security keys, signing credentials, and password hashing, ensuring that your application adheres to best practices for data protection.

#### 7.1. Security Keys and Signing Credentials
The **`SecurityKeyHelper`** and **`SigningCredentialsHelper`** classes are integral to the security architecture of applications that use JSON Web Tokens (JWT) for authentication.

* **SecurityKeyHelper:**
    * **Purpose:** Creates a **`SecurityKey`** from a provided string, which is used in token creation for securing JWTs.
    * **Method:**
        * **CreateSecurityKey(string securityKey):** Converts the provided string into a symmetric security key using UTF-8 encoding.
    * **Usage:** This key is used in the process of signing JWT tokens to ensure their integrity and that they have not been tampered with.

* **SigningCredentialsHelper:**
    * **Purpose:** Creates signing credentials that are used to sign JWT tokens.
    * **Method:**
        * **CreateSigningCredentials(SecurityKey securityKey):** Takes a **`SecurityKey`** and returns **`SigningCredentials`** using the HMAC SHA-256 algorithm.
    * **Usage:** Signing credentials are a critical part of JWT creation, ensuring that tokens are securely signed and verifiable.

These helpers streamline the process of setting up secure JWT tokens by abstracting the creation of keys and credentials, making the security setup straightforward and less error-prone.

#### 7.2. Password Management with **`PasswordHelper`** and **`PasswordGenerator`**

Password security is another critical area, and the **`PasswordHelper`** and **`PasswordGenerator`** classes provide comprehensive solutions for hashing, verifying, and generating passwords.

* **PasswordHelper:**
    * **Purpose:** Handles the creation and verification of password hashes using HMAC SHA-512, a secure hashing algorithm.
    * **Methods:**
        * **`CreatePasswordHashByHmacSha512(string password, out byte[] passwordHash, out byte[] passwordSalt):`**
            * Generates a hash and a salt for the given password using HMAC SHA-512.
            * The salt is generated by the HMAC object itself, enhancing security by ensuring each password has a unique salt.
        * **`VerifyPasswordHashByHmacSha512(string password, byte[] passwordHash, byte[] passwordSalt):`**
            * Verifies a password against its hash and salt by recomputing the hash and comparing it to the stored hash.
            * Ensures that the password is validated correctly without exposing the raw password or hash.
        * **`CreatePasswordByHmacSha512(string password):`**
            * A convenience method that creates both a password hash and salt, returning them as a tuple.
    * **Usage:** These methods ensure that passwords are stored securely, only ever as hashes and salts, never in plain text, and provide a robust way to validate passwords during login attempts.

* **PasswordGenerator:**
    * **Purpose:** Generates random, strong passwords of specified length using a mix of alphanumeric characters and symbols.
    * **Method:**
        * **`GeneratePassword(int length = 8):`**
            * Generates a random password of a specified length (defaulting to 8 characters) using a cryptographically secure random number generator.
            * Throws an exception if the requested length is less than 1, ensuring valid input.
    * **Usage:** Useful for creating strong passwords for users automatically, which can be utilized in scenarios like initial account setups or password resets.

By incorporating these encryption and password management tools, your application leverages secure standards to protect sensitive data, providing strong authentication mechanisms and safeguarding against common vulnerabilities. The use of HMAC SHA-512 for password hashing and the abstraction of security key and credential creation simplifies secure coding practices, helping developers build secure applications more easily.

<br/>

### 8. API Responses

Handling API responses in a structured and consistent manner is critical for building reliable and user-friendly applications. The **`CustomApiResponse<T>`** class offers a flexible and reusable way to standardize API responses across the application. This class ensures that API responses are predictable and informative, making them easier to consume by clients.

**Key Features of **`CustomApiResponse<T>`****
* **Generic Type:** The class is generic and can handle any type of data (T). This flexibility allows it to be used across various endpoints and scenarios without needing specific implementations for each data type.
* **Properties:**

    * **StatusCode:** Represents the HTTP status code of the response. This helps clients understand the nature of the response, such as success (200), not found (404), or server error (500).
    * **IsSuccessful:** A boolean flag indicating whether the operation was successful. This provides a quick check for clients to determine if they need to handle errors.
    * **StatusMessage:** A human-readable message that describes the status of the operation. It can be customized or default to pre-defined system messages.
    * **Data:** Holds the data of the response when the operation is successful. If the operation fails, this property will typically be null.
    * **ErrorMessages:** A list of error messages explaining why the operation failed. This list can be used to provide detailed feedback to the client, allowing for better error handling and user experience.

**Usage Scenarios**

1. **Success Responses:**

**No Data:** Use **`CustomApiResponse<T>.Success(int statusCode, string? statusMessage = null)`** when the operation is successful but there is no specific data to return. This is often used for operations like deletions or updates where the action completes without returning an entity.
**With Data:** Use **`CustomApiResponse<T>.Success(int statusCode, T data, string? statusMessage = null)`** when the operation is successful and there is data to return. This is useful for GET requests where the client expects an entity or a list of entities.

2. **Failure Responses:**

* **Single Error Message:** Use **`CustomApiResponse<T>.Fail(int statusCode, string errorMessage, string? statusMessage = null)`** when the operation fails and you want to provide a single error message. This is helpful for common errors where one primary message is sufficient.
* **Multiple Error Messages:** Use **`CustomApiResponse<T>.Fail(int statusCode, List<string> errorMessages, string? statusMessage = null)`** when the operation fails and there are multiple reasons for the failure. This approach is particularly useful for validation errors where multiple fields might have issues.

**Benefits**
* **Consistency:** By using a standard API response format, clients can rely on consistent structure across all endpoints, making integration easier.
* **Clarity:** The separation of status, data, and errors allows clients to handle responses effectively without needing to parse or guess the nature of the response.
* **Extensibility:** The use of generics allows this class to be adapted to various types of responses without needing to create new response classes for different data types.
* **Ease of Debugging:** Detailed error messages provide clear insights into what went wrong, aiding in faster debugging and resolution of issues.
* **Localization and Custom Messages:** By allowing custom status messages, you can provide localized or more descriptive messages that suit the context of your application.

* **Example Usage**

**Success Response with Data:**
``` C#
var response = CustomApiResponse<ProductDto>.Success(200, productDto, "Product fetched successfully.");
```

**Failure Response with Single Error:**
``` C#
var response = CustomApiResponse<NoData>.Fail(400, "Invalid product ID.");
```

**Failure Response with Multiple Errors:**
``` C#
var errors = new List<string> { "Invalid product ID.", "Product not found." };
var response = CustomApiResponse<NoData>.Fail(404, errors);
```

Using **`CustomApiResponse<T>`** simplifies error handling and data return strategies, allowing both server-side developers and client-side consumers to work with clear and structured API communications.

<br/>

<h2 align="center">⚒️ Languages-Frameworks ⚒️</h2>

In this project, several libraries are utilized to enhance functionality, improve code quality, and streamline development:

* **`Microsoft.EntityFrameworkCore.SqlServer:`** Used for working with SQL Server databases in Entity Framework Core, providing an ORM layer for data access.

* **`FluentValidation.AspNetCore:`** A popular library for validating models in .NET applications. It integrates seamlessly with ASP.NET Core, enabling model validation through fluent API rules.

* **`Serilog.AspNetCore:`** A logging library that offers structured logging for .NET applications. It is highly configurable and supports various sinks, making it easy to log data to different outputs like files, databases, and third-party services.

* **`Autofac.Extensions.DependencyInjection:`** A dependency injection library that enhances the default DI container in ASP.NET Core. It provides more advanced features such as property injection, module configuration, and dynamic scanning of assemblies for dependencies.

* **`Autofac.Extras.DynamicProxy:`** Used for aspect-oriented programming (AOP) in conjunction with Autofac. This library allows for the creation of interceptors, which can add cross-cutting concerns like logging, caching, or validation without modifying the core business logic.

These libraries together form the backbone of the application's architecture, facilitating robust data handling, validation, logging, and dependency management.

<br/>

<div align="center">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="55" alt="csharp logo" />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dot-net/dot-net-plain-wordmark.svg" height="55" alt="dot-net logo" />
</div>
