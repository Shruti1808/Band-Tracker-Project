# C#: Band Tracker
#### A site for storing venues and bands that play there.
## Using SQL database to
#### By _Shruti Priya_

### Description
_This program is a web application designed for tracking a band. It will allow the user to add bands to a venue. It will also allow them to view the list of bands that played at a particular venue and vice-versa. The user can edit and delete any venue.

### Specifications


### Setup/Installation Requirements
1. Clone this repository
 2. Run "DNU restore" on PowerShell in the cloned repository.

* View "http://localhost:5004" in your default web browser

#### Step 2

Create the database using the following commands in PowerShell's SQLCMD(sqlcmd -S "(localdb)\mssqllocaldb"):

* **CREATE DATABASE band_tracker;**
* **GO**
* **USE band_tracker;**
* **GO**
* **CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255), venue_id INT);**
* **GO**
* **CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));**
* **GO**
* **CREATE TABLE venues_bands (id INT IDENTITY(1,1), venue_id INT, band_id INT);**
* **GO**

### Known Bugs
No known bugs yet.

### Support and Contact Details
You can reach me via email: **shrutipriya1808@gmail.com**

### Technologies Used
* C#
* Nancy framework
* Razor
* SQL

#### License
This software is licensed under the MIT license.

Copyright (c) 2017 **_(Shruti Priya)_**
