
## Demo project for Ebiscon

### Prerequirements

##### Have SQL Server or SQL Express installed
##### Have .net 8 installed

### Change connection string!

### Setup Admin user

User API and chage role in DB to 'Admin' OR use this scrypt instead

` INSERT INTO Users (Email, Password, UserType, FirstName, LastName, IsDeleted)
  VALUES ('admin@email.com', '$2a$11$NvkY5pny0IY8RUFdhIkB/e4ca6roXhELE/18DsBFOImYZa8op.6V.', 'Admin', 'Mr', 'Boss', 0)
  -- pass: pass123#A`

### Database structure
![Database structure](/DB-structure.png)