Inputs(Target Portfolio Allocation, Current Holdings, Current Price Information)

Step 1: Identify if any currencies are out of balance by configured amount relative 
to the base currency.  Do this by calculating an acceptable range for each currency
 based on the base currencies current balance.  If none are out of balance return a
 list of no trades.

Step 2:  If any currencies are out of balance use modulo arithmetic to determine 
how much each currency should be bought or sold.  

Step 3: Use current prices to simulate the trades and run the balancing algorithm 
again.  Repeat until no currencies have to be rebalanced.  This will ensure the 
minimum number of trades will be executed.

Step 4: Sort the required trades so that SELL orders are executed first, and 
BUY orders are executed last to ensure adequate base currency will be available
 for the trade.

Step 5: Return the list of suggested trades to the method caller.