import System.IO
import Data.List

main = do
  contents <- readFile "input.txt"
  print . length . filter (\xs -> nub xs == xs) . map words $ lines contents
  print . length . filter (\xs -> nub xs == xs) . map (map sort) . map words $ lines contents
