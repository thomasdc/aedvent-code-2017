import System.IO

calculateDiff :: [Int] -> Int
calculateDiff xs = (maximum xs) - (minimum xs)

isDivisibleValue :: (Int, [Int]) -> Bool
isDivisibleValue (a, []) = False
isDivisibleValue (a, (x:xs)) 
  | a > x  = if a `rem` x == 0 then True else isDivisibleValue (a, xs)
  | a < x  = if x `rem` a == 0 then True else isDivisibleValue (a, xs) 
  | a == x = isDivisibleValue(a, xs)

calculateDivision :: ([Int], [Int]) -> Int
calculateDivision ((x:y:[]), zs) = if x >= y then x `div` y else y `div` x
calculateDivision ((x:xs), zs)   = if isDivisibleValue (x, zs) then calculateDivision ((xs ++ [x]), zs) else calculateDivision (xs,zs)  

main = do
  contents <- readFile "input.txt"
  let contentsParsed = map (map (read :: String -> Int)) . map words $ lines contents 
  print . sum . map calculateDiff $ contentsParsed
  print . sum . map (\x -> calculateDivision (x,x)) $ contentsParsed  
