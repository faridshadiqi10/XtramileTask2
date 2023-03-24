# XtramileTask2
Instruction:
- .Net Core 3.1
- Download the source code
- Just run the code

## TASK 1: Data structure problem solving 

Please note: This task does not require code to be written.
Assuming we have a large set of patients (500,000+) in a relational database, we want the user 
to type part of a patient name and the system returns a list of matched patients.
• What data structure and search/matching algorithm to use and why?
• How fast the search is using this algorithm and data structure?

## Answer 

For the algorithm i will consider Trie and Elastic Search these answer is based on my experience i had.
The decesion will apply depend on the case we faced. If i wanted to search and matching on patient names i prefer to use trie because it more straightforward and effecient. That because in Trie its working as prefix-based search. and it will be used for matched on the prefix of a string. 
Tries also can customisable in the application code withour relying on external libraries in .Net

If I wanted to have complicated search or multiple field needed for the requirement i prefer using Elastic search. The Elastic search have full text search engine and have provide advanced search capabilities. But in term of develpment migh be need to have some of library installation, configuration and maintained.

Tries : 
Quitely fast because is using prefix based search. The time is depend of the length of the query.

Elastic Search:The search speed depends on various factors, such as the size of the index, the complexity of the query, and the hardware resources available. it has a features for tuned up and optimized for faster search performance by adjusting settings like index sharding, replicas, and caching.
