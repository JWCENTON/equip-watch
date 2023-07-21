import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import ClientCreateFormView from '../../components/client/clientCreateForm/ClientCreateFormView';

const ClientCreateForm = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <ClientCreateFormView />
            </div>
        </div>
    );
};

export default ClientCreateForm;