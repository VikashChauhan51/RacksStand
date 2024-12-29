import  { useState, useEffect,ReactNode } from 'react';

type NetworkStatusProps= {
    children:ReactNode
}
const NetworkStatus = ({ children }:NetworkStatusProps) => {
    const [isOnline, setIsOnline] = useState(navigator.onLine);

    const updateNetworkStatus = () => {
        setIsOnline(navigator.onLine);
    };

    useEffect(() => {
        window.addEventListener('online', updateNetworkStatus);
        window.addEventListener('offline', updateNetworkStatus);

        return () => {
            window.removeEventListener('online', updateNetworkStatus);
            window.removeEventListener('offline', updateNetworkStatus);
        };
    }, []);

    if (!isOnline) {
        return (
            <div>
                <h1>Network Error</h1>
                <p>You are currently offline. Please check your internet connection.</p>
            </div>
        );
    }

    return children;
};

export default NetworkStatus;
