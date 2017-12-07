import System.IO
import System.IO
import Data.List

hasDuplicates :: [String] -> Bool
hasDuplicates xs = nub xs == xs 

main = do
  contents <- readFile "input.txt"
  print . length . filter hasDuplicates . map words $ lines contents
