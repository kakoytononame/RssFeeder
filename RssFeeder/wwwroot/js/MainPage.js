function getNews(){
   var title= document.getElementsByClassName("Title");

   fetch('/news/get?number=0', {
      method: 'GET',
      headers: {
         'Content-Type': 'application/json'
      }
   })
       .then(response => response.json())
       .then(result => {
           console.log(result)
           title[0].innerHTML=result
       })
       .catch(error => {
          // Обработка ошибки
          console.error('Error:', error);
       });
}