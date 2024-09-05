# live server of the APIs link : 
[Live Server](http://ec2-34-250-251-119.eu-west-1.compute.amazonaws.com/swagger/index.html)

# Doctor portal: uploading case images

## About Upload Case Images

### As a doctor, I need to upload case images, select the anatomy, and to be able to view the history of the case

# Scenario 1: Uploading Case Image

## ● Given that the doctor is redirected to create a new request successfully, then he should view a new tab for Uploading Case Image with the below fields:
### 1. Add case image: 
#### ● Given that the doctor clicked on the upload image option in the add case image field, he will find two options to upload the case image either via camera or from the Device, when he clicks from device, then he will choose the needed photo to be uploaded, and press submit.
### 2- Select date -- to choose the observation date of the case (by default will be the prescription creation date) : 
### 3- Patient's Anatomy -- for selecting the afflicted region on the patient's anatomical body.
#### ● Given that the doctor clicks on the patient's anatomy field will be redirected to a popup to choose the part related to the case.
### 4- Doctors' Notes -- free text field asking the doctor to enter any notes.



# Scenario 2: View History
## The doctor should be able to view the history of uploaded images for the patient

# Scenario 3: Observation Follow-up
## ● Given that the doctor clicked on Observation Follow-up when he chooses the case he needs to Follow-up on (the case will be chosen by selecting the case date from a dropdown list), then he will find the below:
### 1. the historical uploaded images -- as the doctor should view the historical data. 
### 2. The historical case date
### 3. The historical case note 
### 4. A place for uploading new photos. 
### 5. The diagram with the part selected from the previous visit. 
### 6. Current Doctor's Notes -- a free text field for the doctor to enter any comments. 
### 7. Current Visit date -- already captured from the system.

Reference UI / UX: 

[View in Figma](https://www.figma.com/proto/5H2Jv0kV8yzr5tvb7mVdyU/LC-Doctor-Portal?page-id=1192%3A20110&type=design&node-id=1252-10940&viewport=1004%2C-2603%2C0.2&t=quDAkEKG8AyRe2JB-1&scaling=scale-down) 
(Right-click and select "Open link in new tab")
