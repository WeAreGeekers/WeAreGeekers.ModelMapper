# WeAreGeekers.ModelMapper

WeAreGeekers.ModelMapper is a project that aims to facilitate the mapping between two complex objects.

#### NOTE:

The project is still under development and testing.  
Over the weeks the project will be updated with new features and bug fixes.  
Everything will be documented in the changelog section.  
If you want to contribute to the project, you are free to do so.  
We are thrilled that the strength of the community allows us to achieve our goal.  
If you want to discuss the project or contact us you can do it by email at **wearegeekers@gmail.com**


## Frameworks Supported

- .NETCore3.1

## Installation

```PowerShell
PM> Install-Package PackageName
```
    
## Code Example

The service can be initialize with the extension method of IServiceCollection called 'AddModelMapper' that required an action with the 'ModelMapper' object where you can set the mapping settings.

Considering that we have two class 'UserModel' and 'UserResponse' with this settings:

```csharp
class UserModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}

class UserResponse
{
    public long IdUser { get; set; }
    public string UserName { get; set; }
}
```

We can set the map through properties in this way:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add some services..
    
    services.AddModelMapper(modelMapper =>
    {
        modelMapper
            .Map<UserModel, UserResponse>(modelBuilder =>
            {
                modelBuilder
                    .MapProperty(um => um.Id, ur => ur.IdUser)
                    .MapProperty(um => um.Name, ur => ur.UserName);
            });
            
        // Use .Map method to set other mapping
        
    });
    
    // Add other services...

}
```

At the end we can use the method 'MapTo<TObject>();' for mapping an object to another that we set in the service.

```csharp
UserModel um = new UserModel()
{
    Id = 1,
    Name = "Geekers",
    CreatedAt = DateTime.Now
};

// Convert the 'UserModel' object into 'UserResponse' object
UserResponse ur = um.MapTo<UserResponse>();
```

## Changelog

- 07.01.2021: Publish v. 0.0.1-beta1
    
## Future Features

- Custom converter through types
