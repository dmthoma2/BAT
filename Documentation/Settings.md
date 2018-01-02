Credentials for Binance API
Rebalance Threshold: 20 //Integer, represents the percent an asset can be from the base currency before triggering a rebalance.
Base Currency: BTC or ETH //String, currency trading symbol.  Base currency must have direct market into all others in portfolio.
Base Currency Allocation: 20 //Integer value of 10 to 90 representing percent of portfolio to allocate to base currency.  All allocations must total to 100,
Base Currency Initial Allocation: //Decimal, initial position before trading algorithm.
Currency 1: //String, currency trading symbol.
Currency 1 Allocation: //Integer value of 10 to 90, percentage of portfolio to allocation to this currency.  
Currency 1 Initial Allocation: //Decimal, initial position before trading algorithm.
Currency 2: //String, currency trading symbol.
Currency 2 Allocation: //Integer value of 10 to 90, percentage of portfolio to allocation to this currency.
Currency 2 Initial Allocation: //Decimal, initial position before trading algorithm.
Currency 3: //String, currency trading symbol.
Currency 3 Allocation: //Integer value of 10 to 90, percentage of portfolio to allocation to this currency.
Currency 3 Initial Allocation: //Decimal, initial position before trading algorithm.
Currency 4: //String, currency trading symbol.
Currency 4 Allocation: //Integer value of 10 to 90, percentage of portfolio to allocation to this currency.
Currency 4 Initial Allocation: //Decimal, initial position before trading algorithm.
Use Circuit Breaker: //If yes, will prevent trading if a certain # trades has occurred in the last # of hours.
Circuit Breaker Trades: 3 //Number of trades within Circruit Breaker period to prevent operation.
Circuit Breaker Hours: 24 //Number of hours to look back when determining to trip trading prevention. 
Information Email Address: abc@123.com//Email address to send status information to.
Loading Email: true //Indicates to email when the application loads.  Will always be sent if a circuit breaker is tripped.
BuyAndHoldComparison: //Include calculations to compare algorithm results to Buy and Hold strategy
Algorithm Email: true //Indicates to email when algorithm has executed
ExecutionEmail: true //Indicates to email after trades have been executed.  Will include success and error information.
FailOnError: true //Indicates if the program will not run if an is reported in operation log within the circuit breaker hours.