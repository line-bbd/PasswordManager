# Password Manager

A very basic, single file program that can:

- generate and store a password. Passowrd is made by generating an SHA256 hash of the service name.

- store an existing password.

- retrieve an existing password.

## Note:

- I added TODO's for certain flow aspects (like looping when incorrect input is given or null value checking). I didn't implement any of the looping because I tried keeping everything abstracted to single method calls for now since I know we're going to be breaking the flow up into more complex classes and calls. This is purely to have a skeleton for a basic flow of the program for a single user.

- If you want to test the retrieval, there are two services I have already stored passwords for. The one is 'youtube' and the other is 'paypal'. Retrieving 'paypal' will show you a generated hash password.
