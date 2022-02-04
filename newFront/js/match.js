
//const url = "https://localhost:7012";
"use strict";

//let getRoute = `${url}/Rencontre`;

const getrout = 'https://localhost:7012/Rencontre';

function getAllGames(url) {

    fetch(url, {

        headers: {
            "Accept": "application/json",
            //"Accept": "text/plain",
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest"
            //,'Authorization': 'Bearer ${inMemoryToken}'
        },
        
        method: "GET"
    })

    //Récupération de la réponse et conversion de la réponse au format json
    //.then(response => response.text())
    .then(response => response.json())


    //Affiche une alerte avec la réponse au format texte
    .then(data => {
        //console.log('success:' , data);

        

        const root = document.getElementById("inputScript");


        for(let item of data)
        {

            

            

            let tableau = document.createElement('table'); 
            root.appendChild(tableau);
            tableau.className = "tableau";
            tableau.id =  `tab${item.id}`;
            let capt = document.createElement('caption'); 
            capt.innerText = `LE MATCH DU ${item.dateRencontre}`;



            const headClass = ["date_rencontre", "equipe1", "equipe2" ]
            const celClass = ["date_rencontre", "score1", "score2" ]
            let tr = document.createElement('tr');
            let tbody = document.createElement('tbody');
            tbody.className = "corps_tableau";


            tableau.appendChild(capt);
            tableau.appendChild(tr);
            tableau.appendChild(tbody);

           for(let i = 0; i< celClass.length; i++)
           {

                let th = document.createElement('th');
                let td = document.createElement('td');
                let di1 = document.createElement('div');
                let di2 = document.createElement('div');
                di1.className = headClass[i];
                di2.className = celClass[i];

                if(i === 0){ 
                    
                    di1.innerText = "Date de la rencontre";
                    di2.innerText = item.dateRencontre;
                }
                else if( i === 1)
                {


                    di1.innerText = item.equipe1;
                    di2.innerText = item.score1;
                }
                else{
                    di1.innerText = item.equipe2;
                    di2.innerText = item.score2;
                }
                tr.appendChild(th);
                tbody.appendChild(td);
                th.appendChild(di1);
                td.appendChild(di2);

           }

        }
        
    })
    .catch((error) => {
        console.error('Error :', error);
    });

}

window.addEventListener('load', (e) => {
    
    getAllGames(getrout);
    
});