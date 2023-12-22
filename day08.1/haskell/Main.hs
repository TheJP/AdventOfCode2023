module Main where

main :: IO ()
main = (print . (let {step i cur ((k, (l,r)):ms) | k == cur = if (i=='L') then l else r | otherwise = step i cur ms; round [] _ cur = cur; round (i:is) m cur = round is m $ step i cur m; loop steps _ is "ZZZ" = steps * length is; loop s m is cur = loop (s + 1) m is $ round is m cur} in (\(i, m) -> loop 0 m i "AAA")) . (\(x:_:xs) -> (x, map (\ys -> (take 3 ys, (take 3 $ drop 7 ys, take 3 $ drop 12 ys))) xs)) . lines) =<< readFile "task.in"
