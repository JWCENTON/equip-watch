import React from 'react';
import { Card } from 'react-bootstrap';
import './ClientCard.css';

function ClientCard({ name, address, phone }) {
    return (
        <>
            <Card className="card">
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Client Name</Card.Title>
                    <Card.Text>{name}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Contact Address</Card.Title>
                    <Card.Text>{address}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Phone</Card.Title>
                    <Card.Text>{phone}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Most Recent Comission</Card.Title>
                    <Card.Text>{phone}</Card.Text>
                </div>
            </Card>
        </>
    );
}

export default ClientCard;