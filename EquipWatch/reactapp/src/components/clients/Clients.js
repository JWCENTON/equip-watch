import React from 'react';
import './Clients.css';
import ClientCard from '../clientCard/ClientCard';

function Client() {

    let clients = GetClientData();

    return (
        <div className="clientSection">
            <a className="myAndAllSwitch" href="/" >My clients</a> | <a className="myAndAllSwitch" href="/" >All clients</a>
            <div className="clientContainer">
                {
                    clients != null
                        ?
                    <p>Loading...</p>
                        :
                    clients.map((client) => (<ClientCard name={client.Name} address={client.ContactAddress} phone={client.Phone} recentCommission={client.RecentCommission}></ClientCard>))
                }
            </div>
        </div>
    );
}

async function GetClientData() {
    const response = await fetch('client');
    console.log(response);
    const data = await response.json();
    console.log(data);
    return data;
}

export default Client; 
