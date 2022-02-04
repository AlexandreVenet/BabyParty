"use strict";

const uri = 'https://localhost:7012';

let getAllButton;

function UseURI(str)
{
	return `${uri}${str}`; 
}

function CallGet(e)
{
	e.preventDefault();
    console.log(e.target);

	fetch(UseURI('/Rencontre'))
		.then (response => response.json())
		.then (data => CallGetAllDisplayContent(data))
		.catch(error => console.error('Get ne fonctionne pas', error));
}

function CallGetAllDisplayContent(data)
{
    console.log(data);
}

window.addEventListener('load', () => {

    getAllButton = document.getElementById('getAllButton');
	getAllButton.addEventListener('click', CallGet, false);

});