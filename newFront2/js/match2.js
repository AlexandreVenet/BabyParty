"use strict";

const url = "https://localhost:7012";


const getRoute = `${url}/Rencontre`;



function getAllGames(route) {

    fetch(route, {

        headers: {
            "Accept": "application/json",
            //"Accept": "text/plain",
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest"
            //,'Authorization': 'Bearer ${inMemoryToken}'
        },
        
        method: "GET"
    })

    .then(response => response.json())


    .then(data => {
        console.log('success:' , data);

        

        const root = document.getElementById("inputScript");


        for(let item of data)
        {
            let tableau = document.createElement('table'); 
            root.appendChild(tableau);
            tableau.className = "tableau";
            
            
            let capt = document.createElement('caption'); 
            capt.innerText = `LE MATCH DU ${formatDateFr(item.dateRencontre)}`;

            const headClass = ["date_rencontre", "equipe1", "equipe2" ]
            const celClass = ["date_rencontre", "score1", "score2" ]
            let tr = document.createElement('tr');
            let tbody = document.createElement('tbody');
            tbody.className = "corps_tableau";


            if( root.dataset.adm == "true"){

                const bar = document.createElement('div');
                bar.classname= 'w3-bar';
                let updBtn = document.createElement('button');
                updBtn.dataset.id = item.id;
                let delBtn = document.createElement('button');
                updBtn.className="updateBtn w3-bar-item w3-btn w3-round-large w3-blue ";
                

                delBtn.className="w3-bar-item w3-btn w3-round-large w3-red ";
                delBtn.dataset.id = item.id;
                bar.appendChild(updBtn);
                bar.appendChild(delBtn);
                tableau.appendChild(bar);
            }


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
                    di2.innerText = `Le ${formatDateFr(item.dateRencontre)}`;
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
function getOne(route , match ) {

    fetch(`${route}/${match.id}`, {

        headers: {
            "Accept": "application/json",
            //"Accept": "text/plain",
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest"
            //,'Authorization': 'Bearer ${inMemoryToken}'
        },
        
        method: "GET"
    })
    .then(response => response.json())

    .then(data => {

        const root = document.getElementById("inputGetOne");
        
        item = data[0];
        match['date'] = item.dateRencontre;
        match['eq1'] = item.equipe1;
        match['eq2'] = item.equipe2;
        match['sc1'] = item.score1;
        match['sc2'] = item.score2;
  
    })
    .catch((error) => {
        console.error('Error :', error);
    });

}
async function handleSubmitForm(event, url, form, id) 
{   
    event.preventDefault();

    const formData = new FormData(form);
    const data = {};
    const meth = id ? 'PUT' : 'POST';
    if(meth === 'PUT'){ data['id'] = id }
    formData.forEach((value, key) => (data[key]= value));
    
    try {
        const response = await fetch(url, {
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "X-Requested-With": "XMLHttpRequest"
                //,'Authorization': 'Bearer ${inMemoryToken}'
            },

            method: meth,

        
            body: JSON.stringify(data)
            });
        if(!response.ok) {
            const mess = 'Error status code:' + response.status;
            throw new Error(mess);
        }
        const resp = await response.json();
        console.log(resp);
        
    } catch (error) {

        console.log('Error: ' + error);
        
    }
}



function parseDate(str){
    let arr = str.split('/');
    let newStr = `${arr[1]}/${arr[0]}/${arr[2]}`;
    return new Date(newStr);
}

function formatDateFr(str){
    const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    return parseDate(str).toLocaleDateString('fr-FR', options);
}

window.addEventListener('load', (e) => {
    
    getAllGames(getRoute);
    
});

let addForm = document.getElementById('post-form');
addForm.addEventListener('submit', (e) => {
    handleSubmitForm(e, getRoute , addForm, null);
});

// let updateBtn = document.querySelector('.updateBtn');
// updateBtn.addEventListener('click' , (e) => {
//     const match = {id : this.dataset.id};
//     getOne(route, match);
//     console.log( match);
    


// });

let updateForm = document.getElementById('put-form');
updateForm.addEventListener('submit', (e) => {
    handleSubmitForm(e, getRoute, updateForm, id);
});

