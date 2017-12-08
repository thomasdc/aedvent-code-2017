import System.IO

replaceNth :: Int -> Int -> [Int] -> [Int]
replaceNth n v (x:xs)
     | n == 0    = v:xs
     | otherwise = x : replaceNth (n-1) v xs

calculateNumberOfSteps :: [Int] -> Int -> Int -> Int
calculateNumberOfSteps xs steps index 
  | index >= length xs = steps 
  | index < 0          = steps
  | otherwise          = calculateNumberOfSteps (replaceNth index ((xs !! index) + 1) xs) (steps + 1) (index + (xs !! index))   

main = do
  contents <- readFile "input.txt"
  let contentsParsed = map (read :: String -> Int) $ lines contents 
  print $ calculateNumberOfSteps contentsParsed 0 0
