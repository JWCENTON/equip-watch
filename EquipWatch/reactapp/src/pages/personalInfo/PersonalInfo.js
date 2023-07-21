import React, { useContext } from 'react';
import { SidebarContext } from '../../contexts/SidebarContext';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import PersonalInfo from '../../components/personalInfo/PersonalInfo';

const PersonalInfoPage = () => {
    const { toggleSidebar } = useContext(SidebarContext);
    return (
        <div className="app-container">
            <Navigation onMenuClick={toggleSidebar} />
            <div className="main-container">
                <Sidebar />
                <PersonalInfo className="main-content" />
            </div>
        </div>
    );
};

export default PersonalInfoPage;