let jsonObject;
function getNews(number){
    document.getElementById("filter").style.visibility="visible";
    cleartable();
    let table = document.getElementById("myTable");
    let token = Cookies.get('AuthToken');
    if(token===undefined||token===null){
        window.location.href="/Home/Login"
    }
    var payload = JSON.parse(atob(token.split('.')[1]));
    var role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if(role==="admin"){
        document.getElementById("addNews").style.visibility="visible";
    }
    else {
        document.getElementById("addNews").style.visibility="hidden";
    }
    
   fetch('/news/get', {
      method: 'GET',
      headers: {
         'Content-Type': 'application/json', 
          'Authorization': 'Bearer ' + token
      }
   })
       .then(response => {
               if (response.status === 401) {
                   // Обработка ошибки авторизации (401)
                   window.location.href = "/Home/Login";
               }
           return response.text();
       })
       .then(jsonString => {
           
           jsonObject = JSON.parse(jsonString);
           // Далее работайте с объектом JSON
           jsonObject.forEach(function(item) {
               item.pubDate = new Date(item.pubDate);
               item.dbAdded=new Date(item.dbAdded);
           });
           jsonObject.sort(function(a, b) {
               return a.pubDate - b.pubDate;
           });
           console.log(jsonObject);
           for(let i=0;i<jsonObject.length;i++){
               let row = table.tBodies[0].insertRow();
               let cell = row.insertCell();
               let link = document.createElement('a');
               link.id="Link"+jsonObject[i].itemId;
               link.innerHTML+="Добавлено в бд "+jsonObject[i].dbAdded+"<br>"
               link.innerHTML+=jsonObject[i].title+"<br>"
               link.innerHTML+="Источник " +jsonObject[i].link
               if(jsonObject[i].readedFilter==="readed"){
                   link.style.color="blue";
               }
               if(jsonObject[i].readedFilter==="unreaded") {
                   link.style.color="black"
               }
               cell.appendChild(link);
               
           }
           document.body.addEventListener( 'click', function ( event ) {
               if( event.target.id.includes('Link') ) {
                   readnews(event.target)
               };
           });
           let selectElement = document.getElementById('filter');

           selectElement.addEventListener('change', function(event) {
               let selectedOption = event.target.value;
               if(selectedOption==="Все"){
                   getNews(number);
               }
               if(selectedOption==="Прочитанные"){
                   addfilter(1);
               }
               if(selectedOption==="Непрочитанные"){
                   addfilter(2);
               }
               // Дополнительная обработка выбранного варианта
           });    
           
       })
       .catch(error => {
          // Обработка ошибки
          console.error('Error:', error);
       });
}

function readnews(element){
    let table = document.getElementById("myTable");
    document.getElementById("filter").style.visibility="hidden";
    let readedObject=jsonObject.find(item=>item.itemId===element.id.toString().replace('Link',''));
    console.log(readedObject);
    if(readedObject!=null||readedObject!=undefined){
        cleartable();
        let data={
            RssItemId:readedObject.itemId,
        }
        let token = Cookies.get('AuthToken');
        fetch('/news/read', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body:JSON.stringify(data)
            })
            .then(response => response.text())
            .then(result=>{
                
                    let row = table.tBodies[0].insertRow();
                    let cell = row.insertCell();
                    let link = document.createElement('a');
                    link.innerHTML+="Добавлено в бд "+readedObject.dbAdded+"<br>";
                    link.innerHTML+=readedObject.title+"<br>";
                    link.innerHTML+="Описание<br>"+readedObject.description;
                    link.innerHTML+="Дата публикации"+readedObject.pubDate+"<br>";
                    link.innerHTML+="Источник " +readedObject.link;
                    cell.appendChild(link);
            })
    }
}

function addfilter(variant){
    
    if(variant===1){
        let table = document.getElementById("myTable");
        cleartable();
        let token = Cookies.get('AuthToken');
        fetch('/news/readed', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        })
            .then(response => response.text())
            .then(jsonString => {

                let jsonObject1 = JSON.parse(jsonString);
                // Далее работайте с объектом JSON
                jsonObject1.forEach(function(item) {
                    item.pubDate = new Date(item.pubDate);
                    item.dbAdded=new Date(item.dbAdded);
                });
                jsonObject1.sort(function(a, b) {
                    return a.pubDate - b.pubDate;
                });
                for(let i=0;i<jsonObject1.length;i++){
                    let row = table.tBodies[0].insertRow();
                    let cell = row.insertCell();
                    let link = document.createElement('a');
                    link.id="Link"+jsonObject1[i].itemId;
                    link.innerHTML+="Добавлено в бд "+jsonObject1[i].dbAdded+"<br>"
                    link.innerHTML+=jsonObject1[i].title+"<br>"
                    link.innerHTML+="Источник " +jsonObject1[i].link
                    cell.appendChild(link);
                    cell.style.backgroundColor = 'gray';
                }
                document.body.addEventListener( 'click', function ( event ) {
                    if( event.target.id.includes('Link') ) {
                        readnews(event.target)
                    };
                });

            });
    }
    if(variant===2){
        let table = document.getElementById("myTable");
        cleartable();
        let token = Cookies.get('AuthToken');
        fetch('/news/unreaded', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        })
            .then(response => response.text())
            .then(jsonString => {

                let jsonObject1 = JSON.parse(jsonString);
                // Далее работайте с объектом JSON
                jsonObject1.forEach(function(item) {
                    item.pubDate = new Date(item.pubDate);
                    item.dbAdded=new Date(item.dbAdded);
                });
                jsonObject1.sort(function(a, b) {
                    return a.pubDate - b.pubDate;
                });
                for(let i=0;i<jsonObject1.length;i++){
                    let row = table.tBodies[0].insertRow();
                    let cell = row.insertCell();
                    let link = document.createElement('a');
                    link.id="Link"+jsonObject1[i].itemId;
                    link.innerHTML+="Добавлено в бд "+jsonObject1[i].dbAdded+"<br>"
                    link.innerHTML+=jsonObject1[i].title+"<br>"
                    link.innerHTML+="Источник " +jsonObject1[i].link
                    cell.appendChild(link);
                    cell.style.backgroundColor = 'lightgrey';
                }
                document.body.addEventListener( 'click', function ( event ) {
                    if( event.target.id.includes('Link') ) {
                        readnews(event.target)
                    };
                });

            });
    }
}
function cleartable(){
    let table = document.getElementById("myTable");
    while (table.rows.length > 0) {
        table.deleteRow(0); // Удаляем первую строку (индекс 0) до тех пор, пока таблица не станет пустой
    }
}

function UploadRssNews(){
    let Url=document.getElementById("RssUrl").value;
    let token = Cookies.get('AuthToken');
    fetch('/news/add?URL='+Url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })
        .then(response => response.text())
        .then(jsonString => {

            //let jsonObject1 = JSON.parse(jsonString);
            let results=document.getElementById("UploadResult");
            results.innerHTML=" [Загружено,Незагруженно]"+jsonString;
        });
}