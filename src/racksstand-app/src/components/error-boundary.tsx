import React, { Component, ErrorInfo, ReactNode } from 'react';

interface ErrorBoundaryProps {
  children: ReactNode;
  fallbackUI?: ReactNode;
}

interface ErrorBoundaryState {
  hasError: boolean;
  error?: Error;
  errorInfo?: ErrorInfo;
}

class ErrorBoundary extends Component<ErrorBoundaryProps, ErrorBoundaryState> {
  constructor(props: ErrorBoundaryProps) {
    super(props);
    this.state = {
      hasError: false,
    };
  }

  static getDerivedStateFromError(error: Error): ErrorBoundaryState {
    return {
      hasError: true,
      error,
    };
  }

  componentDidCatch(error: Error, errorInfo: ErrorInfo): void {
    this.setState({
      errorInfo,
    });
    console.error('Error caught in ErrorBoundary:', error, errorInfo);
  }

  render() {
    const { hasError, error, errorInfo } = this.state;
    const { children, fallbackUI } = this.props;

    if (hasError) {
      return (
        <div>
          {fallbackUI || (
            <div>
              <h2>Something went wrong.</h2>
              <details style={{ whiteSpace: 'pre-wrap' }}>
                {error && error.message}
                <br />
                {errorInfo && errorInfo.componentStack}
              </details>
            </div>
          )}
        </div>
      );
    }

    return children;
  }
}

export default ErrorBoundary;
