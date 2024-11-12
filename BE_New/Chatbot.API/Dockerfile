# Stage 1: Compile and Build angular codebase

# Use official node image as the base image
FROM node:latest AS build

# Create and set the working directory
WORKDIR /app

# Copy package.json and package-lock.json to install dependencies
COPY ./Chatbot.API/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of your application files
COPY ./Chatbot.API .
COPY ./.env .

# Expose the port your app runs on
EXPOSE 8080

# Start the server
CMD ["npm", "start"]