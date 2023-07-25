import React, { useState } from 'react';
import { Form, FormControl, Dropdown, InputGroup, Button } from 'react-bootstrap';
import { FaSearch } from 'react-icons/fa';
import './Searchbar.css';

const Searchbar = ({ inSidebar }) => { // Add a new prop
    const [category, setCategory] = useState("Equipment");

    const handleSelect = (eventKey) => {
        setCategory(eventKey);
    }

    return (
        <Form inline="true" className={`search-bar ${inSidebar ? 'sidebar-search' : ''}`}>

            <InputGroup>
                <FormControl type="text" placeholder="Search" className="mr-sm-2" />
                <div className="dropdown-wrapper">
                    <Dropdown onSelect={handleSelect}>
                        <Dropdown.Toggle variant="secondary" align="end" id="dropdown-menu-align-end" className="category-dropdown">
                            {category}
                        </Dropdown.Toggle>
                        <Dropdown.Menu className="dropdown-menu">
                            <Dropdown.Item eventKey="Equipment">Equipment</Dropdown.Item>
                            <Dropdown.Item eventKey="Commissions">Commissions</Dropdown.Item>
                            <Dropdown.Item eventKey="Clients">Clients</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>
                </div>
                <Button variant="outline-primary"><FaSearch /></Button>
            </InputGroup>
        </Form>
    );
};

export default Searchbar;