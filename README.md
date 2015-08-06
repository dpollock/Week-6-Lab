# Week-6-Lab
Twitter!

# Microblogging Social Network (Twitter)

## Description
Microblogging Social Network (Twitter)


## Objectives

### Learning Objectives

After completing this assignment, you should…

* Understand nuget and their place in c# development
* Understand Relationships between models
* Understand Personalization
* Understand Authentication
* Understand Pagination


### Performance Objectives

After completing this assignment, you be able to effectively use

* authentication, sessions, and User.Identity
* Bootstrap
* Sql Server Database
* Deploying to Azure
* Validations
* Controllers



## Details

### Deliverables

* A repo containing at least:
  * a User model 
  * a Post model that Users can write
  * a Follow model
* A link to Azure website where you have published it.

### Requirements



## Normal Mode

* Users can signup, and sign in
* User can follow other users
* User can see posts from [User + Friends] in their Timeline
* User can Post posts
* User can unfollow a person
* Site should look nice
* Posts should be paginated
* Data should be seeded using fake data
* Get live and up and working on Azure

            
## Hard Mode
            
* User can view a profile (/users/dpollock)
* Users can block users for being a-holes
* Users can search for posts
* Users can upload a photo of themselves
* Users can add a photo to a post


## Notes

* When logged in, the root URL should show the messages from all the people you follow.
* People can post "messages," "cheeps," or whatever you want to call them. They're tweets, but please don't call them that.
* Getting the list of messages for You + people you follow is tricky'ish. Think of it like this:

```c#
  List<Post> GetTimeLine()
  {  
	var followerIds = db.Following.Where(f=>f.FollowedById == MyUserId).Select(f=>f.Userid);
    var allIds = followerIds.Add(MyUserId);
    return db.Posts.Where(x=> allIds.Contains(x.PostedById)).OrderByDesc(x=>x.CreatedBy);
 }
```

## Additional Resources

* [ASP.NET Identity](http://www.asp.net/identity/overview/getting-started/introduction-to-aspnet-identity)
* [Free Bootstrap Themes](https://bootswatch.com/)
