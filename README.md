# ScalabelSimpleBlog
Archtecture of Scalable .NET Web App for Blog System


User stories:

* As User I want to be able change pages with seleted tag 
* As User I want to comment article 
* As User I want search article 
* As User I want to Add Article article
* As User I want Update my article 

Web Test cases: 
* Add Article 
* Random Browse Blog 
* Comment Random Article 
 

Test result 
duration 10 min

|App/branch | 200 users|  400 Users |
|-----------|---------------------------|-----|
|Web App    | Requests/Sec 4.01|Requests/Sec 6.03 |
|No Async   | Pages/Sec 1.99|Pages/Sec 2.21  |
|ONLY SQL   | Avg. Page Time (sec) 78.4 |Avg. Page Time (sec) 109 |
|   | Avg. Response Time (sec) 44.8  | Avg. Response Time (sec) 62.0 | 
| |Errors: 0 | Errors: 0 |
