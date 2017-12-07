import System.IO

calculateDiff :: [Int] -> Int
calculateDiff xs = (maximum xs) - (minimum xs)

main = do
  contents <- readFile "input.txt"
  let contentsParsed = map (map (read :: String -> Int)) . map words $ lines contents 
  print . sum . map calculateDiff $ contentsParsed
