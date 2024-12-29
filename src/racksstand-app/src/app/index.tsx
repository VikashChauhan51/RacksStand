// src/app/page.tsx
export default function Home() {
  // Simulate an error
  if (typeof window !== 'undefined') {
    throw new Error("Test error");
  }

  return (
    <div>
      <h1>Welcome to Next.js</h1>
    </div>
  );
}
