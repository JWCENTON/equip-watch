import React, { useEffect, useState, useContext } from 'react';
import { SidebarContext } from '../../contexts/SidebarContext';
import { useParams } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import ClientEditView from '../../components/client/ClientEditView';

export default function EditView() {
    const { toggleSidebar } = useContext(SidebarContext);
    const { id, dataType } = useParams();

    const [detailsData, setDetailsData] = useState(null);

    useEffect(() => {
        const fetchDetailsData = async () => {
            const response = await fetch(`https://localhost:7007/api/${dataType}/${id}`);
            const data = await response.json();
            setDetailsData(data);
        };
        fetchDetailsData();
    }, [id]);

    return (
        <div className="app-container">
            <Navigation onMenuClick={toggleSidebar} />
            <div className="main-container">
                <Sidebar />
                <ClientEditView detailsData={detailsData} className="main-content" />
            </div>
        </div>
    );
};