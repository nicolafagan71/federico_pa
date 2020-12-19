def cls
  system('cls')
end


## Domanda 1

$a = []

def calc(arr)
  arr.each do |i|
    if i.class != Array
      $a << i
    else
      calc(i)
    end     
  end   
end

#inserire questo array di esempio  [[1,5,[8]],4]  

puts $a.sort

