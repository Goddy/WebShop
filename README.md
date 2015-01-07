WebShop
=======
A simple webshop as per demonstration of our knowledge of the ASP.NET framework.


#### Beware

As I do not yet have an idea on how to include the roles into the configuration script, quickly execute these commands after a deploy:

    insert into AspNetRoles (Id, Name) values (1, 'admin');
    insert into AspNetUserRoles(userid, roleid) values ((select id from AspNetUsers where name = 'Admin'), 1);
